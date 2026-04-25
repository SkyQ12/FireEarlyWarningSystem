using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Resources.User
{
    public class UpdateUserInfoViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string HomeTown { get; set; }
    }
}
