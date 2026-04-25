using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.Users
{
    public interface IUserRepository
    {
        public Task<User> RegisterNewUserAsync(User user);
        public Task<bool> IsExistUser(string element);
        public Task<List<User>> GetAllUserAsync();
        public Task<User> GetUserByIdAsync(string UserId);
        public Task<User> GetUserByUserNameAsync(string UserName);
        public Task DeleteUser(User user);
        public Task UpdateUserInfoAsync(User user);
        public Task<User> LoginAsync(LoginViewModel user);
    }
}
