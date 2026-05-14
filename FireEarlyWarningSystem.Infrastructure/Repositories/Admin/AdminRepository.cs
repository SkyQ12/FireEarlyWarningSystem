using FireEarlyWarningSystem.Infrastructure.Domain.Context;
using FireEarlyWarningSystem.Infrastructure.Domain.Exceptions;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Admin;
using FireEarlyWarningSystem.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.Admin
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> LoginAsync(AdminLoginViewModel admin)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == admin.AdminName && x.Password == admin.Password && x.Role == "Admin");
            return currentUser != null ? currentUser : throw new ResourceNotfoundException("AdminName or Password is incorrect!");
        }
    }
}
