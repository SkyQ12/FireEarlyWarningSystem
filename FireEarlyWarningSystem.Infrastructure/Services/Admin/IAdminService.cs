using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.Admin;

namespace FireEarlyWarningSystem.Infrastructure.Services.Admin
{
    public interface IAdminService
    {
        public Task<string> Login(AdminLoginViewModel adminloginViewModel);
    }
}
