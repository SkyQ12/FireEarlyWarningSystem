using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Repositories.Admin
{
    public interface IAdminRepository
    {
        public Task<User> LoginAsync(AdminLoginViewModel admin);
    }
}
