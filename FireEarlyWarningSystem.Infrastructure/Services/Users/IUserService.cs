using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Services.Users
{
    public interface IUserService
    {
        public Task<bool> RegisterNewUser(CreateNewUserViewModel userViewModel);
        public Task<List<UserViewModel>> GetAllUsers();
        public Task<UserViewModel> GetUserById(string UserId);
        public Task<UserViewModel> GetUserByUserName(string UserName);
        public Task<bool> DeleteUserById(string UserId);
        public Task<bool> UpdateUserInfo(string userName, UpdateUserInfoViewModel updateViewModel);
        public Task<string> ChangePassword(string Id, PasswordChangeViewModel viewModel);
        public Task<string> ChangePasswordByUserName(string userName, PasswordChangeViewModel viewModel);
        public Task<string> Login(LoginViewModel loginViewModel);
    }
}
