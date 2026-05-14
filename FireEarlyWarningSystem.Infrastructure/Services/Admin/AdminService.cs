using AutoMapper;
using FireEarlyWarningSystem.Infrastructure.Repositories.UnitOfWork;
using FireEarlyWarningSystem.Infrastructure.Repositories.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Admin;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;

namespace FireEarlyWarningSystem.Infrastructure.Services.Admin
{
    public class AdminService : IAdminService
    {
        public IAdminRepository _adminRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }
        private readonly JwtSetting _jwtSetting;

        public AdminService(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtSetting> jwtSetting)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtSetting = jwtSetting.Value;
        }

        public async Task<string> Login(AdminLoginViewModel adminLoginViewModel)
        {
            var user = await _adminRepository.LoginAsync(adminLoginViewModel);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var myClaims = new[]
            {
                new Claim(ClaimTypes.SerialNumber, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                claims: myClaims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
