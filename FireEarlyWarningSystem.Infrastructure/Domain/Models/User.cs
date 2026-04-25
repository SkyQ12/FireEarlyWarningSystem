using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserPhoneNumber { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string HomeTown { get; set; }
        public string Role { get; set; }
        public List<Camera> Cameras { get; set; }

        public User()
        {
        }

        public User(string id, string name, string userName, string password, string userPhoneNumber, string sex, DateTime birthday, string homeTown, string role, List<Camera> cameras)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            Sex = sex;
            Birthday = birthday;
            HomeTown = homeTown;
            Role = role;
            Cameras = cameras;
        }
    }
}
