using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Models
{
    public class Camera
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserPhoneNumber { get; set; }
        public string CameraName { get; set; }


        public double Battery { get; set; }

        public string FireSmokeImage { get; set; }

        //Sensor
        public YoloDetectType YoloDetection { get; set; }
        public double TemperatureValue { get; set; }
        public double SmokeValue { get; set; }
        
        //Flag
        public State CameraState { get; set; }

        public DateTime TimeStamp { get; set; }
        public DateTime RegistationDate { get; set; }
        public List<WarningHistory> WarningHistories { get; set; }
        public User User { get; set; }

        public Camera()
        {
        }

        public Camera(string id, string userId, string userPhoneNumber, string cameraName, double battery, string fireSmokeImage, YoloDetectType yoloDetection, double temperatureValue, double smokeValue, State cameraState, DateTime timeStamp, DateTime registationDate, List<WarningHistory> warningHistories, User user)
        {
            Id = id;
            UserId = userId;
            UserPhoneNumber = userPhoneNumber;
            CameraName = cameraName;
            Battery = battery;
            FireSmokeImage = fireSmokeImage;
            YoloDetection = yoloDetection;
            TemperatureValue = temperatureValue;
            SmokeValue = smokeValue;
            CameraState = cameraState;
            TimeStamp = timeStamp;
            RegistationDate = registationDate;
            WarningHistories = warningHistories;
            User = user;
        }
    }
}
