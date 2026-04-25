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
        public string Sex { get; set; }
        [DataMember]
        public DateTime Birthday { get; set; }
        [DataMember]
        public string HomeTown { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Role { get; set; }

        public CreateNewUserViewModel(string id, string name, string userName, string password, string userPhoneNumber, string sex, DateTime birthday, string homeTown, string role)
        {
            Id = "";
            Name = name;
            UserName = userName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            Sex = sex;
            Birthday = birthday;
            HomeTown = homeTown;
            Role = "User";
        }
    }
}
