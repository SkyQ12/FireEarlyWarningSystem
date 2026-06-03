using AutoMapper;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using FireEarlyWarningSystem.Infrastructure.Repositories.Users;
using Microsoft.Extensions.Options;
using FireEarlyWarningSystem.Infrastructure.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FireEarlyWarningSystem.Infrastructure.Repositories.Cameras;
using FireEarlyWarningSystem.Infrastructure.MqttClients;
using Newtonsoft.Json;

namespace FireEarlyWarningSystem.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }
        public ICameraRepository _cameraRepository { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper { get; set; }
        private readonly JwtSetting _jwtSetting;
        private readonly ManagedMqttClient _mqttClient;

        public UserService(IUserRepository userRepository, ICameraRepository cameraRepository, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtSetting> jwtSetting, ManagedMqttClient managedMqttClient)
        {
            _userRepository = userRepository;
            _cameraRepository = cameraRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtSetting = jwtSetting.Value;
            _mqttClient = managedMqttClient;
        }


        public async Task<bool> RegisterNewUser(CreateNewUserViewModel userViewModel)
        {
            var isexist = await _userRepository.IsExistUser(userViewModel.UserName);
            if (isexist)
            {
                return false;
            }
            else
            {
                var source = _mapper.Map<CreateNewUserViewModel, User>(userViewModel);
                var userEntry = await _userRepository.RegisterNewUserAsync(source);
                return await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var source = await _userRepository.GetAllUserAsync();
            var result = _mapper.Map<List<User>, List<UserViewModel>>(source);
            return result;
        }

        public async Task<UserViewModel> GetUserById(string UserId)
        {
            var source = await _userRepository.GetUserByIdAsync(UserId);
            var result = _mapper.Map<User, UserViewModel>(source);
            return result;
        }

        public async Task<UserViewModel> GetUserByUserName(string UserName)
        {
            var source = await _userRepository.GetUserByUserNameAsync(UserName);
            var result = _mapper.Map<User, UserViewModel>(source);
            return result;
        }

        public async Task<bool> DeleteUserById(string UserId)
        {
            var isexist = await _userRepository.GetUserByIdAsync(UserId);
            if (isexist is not null)
            {
                await _userRepository.DeleteUser(isexist);
                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserInfo(string userName, UpdateUserInfoViewModel updateViewModel)
        {
            var isExist = await _userRepository.IsExistUser(userName);
            if (isExist)
            {
                var user = await _userRepository.GetUserByUserNameAsync(userName);
                if (!string.IsNullOrEmpty(updateViewModel.Name))
                {
                    user.Name = updateViewModel.Name;
                }
                if (!string.IsNullOrEmpty(updateViewModel.UserName))
                {
                    user.UserName = updateViewModel.UserName;
                }
                if (!string.IsNullOrEmpty(updateViewModel.UserPhoneNumber))
                {
                    user.UserPhoneNumber = updateViewModel.UserPhoneNumber;
                }

                await _userRepository.UpdateUserInfoAsync(user);
                var userId = await _userRepository.GetUserIdByUserName(userName);
                var cameras = await _cameraRepository.GetCameraIdByUserId(userId.Id);
                Console.WriteLine($"Camera count = {cameras?.Count}");

                var payload = JsonConvert.SerializeObject(new
                {
                    PhoneNumber = user.UserPhoneNumber
                });

                foreach (var camera in cameras)
                {
                    Console.WriteLine($"Publishing to topic: Camera/{camera.Id}/PhoneNumber with payload: {payload}");
                    await _mqttClient.Publish(
                        $"Camera/{camera.Id}/PhoneNumber",
                        payload,
                        true);
                }

                return await _unitOfWork.CompleteAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<string> ChangePassword(string Id, PasswordChangeViewModel viewModel)
        {
            var isExist = await _userRepository.IsExistUser(Id);
            if (isExist)
            {
                var user = await _userRepository.GetUserByIdAsync(Id);
                if (user.Password != viewModel.CurrentPassword)
                {
                    return "Current password is incorrect";
                }
                else if (string.IsNullOrEmpty(viewModel.NewPassword))
                {
                    return "The new password cannot be left blank"!;
                }
                else if (viewModel.CurrentPassword == viewModel.NewPassword)
                {
                    return "The new password cannot be the same as the current password";
                }
                else
                {
                    user.Password = viewModel.NewPassword;
                    await _userRepository.UpdateUserInfoAsync(user);
                    await _unitOfWork.CompleteAsync();
                    return "Change password successfully!";
                }
            }
            else
            {
                return "Not found user with this Id";
            }
        }
        public async Task<string> ChangePasswordByUserName(string userName, PasswordChangeViewModel viewModel)
        {
            var isExist = await _userRepository.IsExistUser(userName);
            if (isExist)
            {
                var user = await _userRepository.GetUserByUserNameAsync(userName);
                if (user.Password != viewModel.CurrentPassword)
                {
                    return "Current password is incorrect";
                }
                else if (string.IsNullOrEmpty(viewModel.NewPassword))
                {
                    return "The new password cannot be left blank";
                }
                else if (viewModel.CurrentPassword == viewModel.NewPassword)
                {
                    return "The new password cannot be the same as the current password";
                }
                else
                {
                    user.Password = viewModel.NewPassword;
                    await _userRepository.UpdateUserInfoAsync(user);
                    await _unitOfWork.CompleteAsync();
                    return "Change password successfully!";
                }
            }
            else
            {
                return "Not found user with this Id";
            }
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            var user = await _userRepository.LoginAsync(loginViewModel);

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
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> AddCameraForUser(AddCameraForUserViewModel viewModel)
        {
            var isExistUseer = await _userRepository.IsExistUser(viewModel.UserId);
            var isExistCamera = await _cameraRepository.IsExistCamera(viewModel.CameraId);
            if (isExistUseer && isExistCamera)
            {
                var checkCamera = await _cameraRepository.GetCameraByIdAsync(viewModel.CameraId);
                var checkUser = await _userRepository.GetUserByIdAsync(viewModel.UserId);
                if (checkCamera.UserId == checkUser.Id && checkCamera.UserId != "NSX")
                {
                    return "This camera has already been added to this user";
                }
                if (checkCamera.UserId != checkUser.Id && checkCamera.UserId != "NSX")
                {
                    return "This camera has already been added to another user";
                }
                else
                {
                    await _cameraRepository.AssignCamera(viewModel.CameraId, viewModel.UserId);


                    // MQTT

                    var payload = JsonConvert.SerializeObject(new
                    {
                        PhoneNumber = checkUser.UserPhoneNumber
                    });

                    
                    Console.WriteLine($"Publishing to topic: Camera/{viewModel.CameraId}/PhoneNumber with payload: {payload}");
                    await _mqttClient.Publish($"Camera/{viewModel.CameraId}/PhoneNumber", payload, true);
                    

                    await _unitOfWork.CompleteAsync();
                    return "Add camera for user successfully!";
                }
            }
            else
            {
                if (!isExistUseer)
                {
                    return "This user does not exist";
                }
                else
                {
                    return "This camera does not exist";
                }
            }
        }

        public async Task<string> RemoveCameraFormUser(AddCameraForUserViewModel viewModel)
        {
            var isExistUseer = await _userRepository.IsExistUser(viewModel.UserId);
            var isExistCamera = await _cameraRepository.IsExistCamera(viewModel.CameraId);
            if (isExistUseer && isExistCamera)
            {
                var checkCamera = await _cameraRepository.GetCameraByIdAsync(viewModel.CameraId);
                if (checkCamera.UserId != viewModel.UserId)
                {
                    return "This camera does not belong to this user";
                }
                else
                {
                    await _cameraRepository.AssignCamera(viewModel.CameraId, "NSX");

                    // MQTT
                    var checkUser = await _userRepository.GetUserByIdAsync(viewModel.UserId);
                    var payload = JsonConvert.SerializeObject(new
                    {
                        PhoneNumber = "NONE"
                    });

                    Console.WriteLine($"Publishing to topic: Camera/{viewModel.CameraId}/PhoneNumber with payload: {payload}");
                    await _mqttClient.Publish($"Camera/{viewModel.CameraId}/PhoneNumber", payload, true);

                    await _unitOfWork.CompleteAsync();
                    return "Remove camera for user successfully!";
                }
            }
            else
            {
                if (!isExistUseer)
                {
                    return "This user does not exist";
                }
                else
                {
                    return "This camera does not exist";
                }
            }
        }
    }
}
