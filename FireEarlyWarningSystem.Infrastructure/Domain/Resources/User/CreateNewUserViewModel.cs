using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Resources.User
{
    [DataContract]
    public class CreateNewUserViewModel
    {
        [DataMember]
        [JsonIgnore]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string UserPhoneNumber { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Role { get; set; }

        public CreateNewUserViewModel(string id, string name, string userName, string password, string userPhoneNumber, string role)
        {
            Id = "";
            Name = name;
            UserName = userName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            Role = "User";
        }
    }
}
