using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using FireEarlyWarningSystem.Infrastructure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera
{
    public class CameraViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CameraName { get; set; }
        public double Battery { get; set; }
        public string RealtimeCameraLink { get; set; }

        //Sensor
        public AIDetectType AIDetection { get; set; }
        public FlameSensorType FlameSensor { get; set; }
        public double SmokeValue { get; set; }

        //Flag
        public StateType CameraState { get; set; }

        public DateTime TimeStamp { get; set; }
        public DateTime RegistationDate { get; set; }

        public CameraViewModel()
        {
        }

        public CameraViewModel(string id, string userId, string cameraName, double battery, string realtimeCameraLink, AIDetectType aIDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, DateTime timeStamp, DateTime registationDate)
        {
            Id = id;
            UserId = userId;
            CameraName = cameraName;
            Battery = battery;
            RealtimeCameraLink = realtimeCameraLink;
            AIDetection = aIDetection;
            FlameSensor = flameSensor;
            SmokeValue = smokeValue;
            CameraState = cameraState;
            TimeStamp = timeStamp;
            RegistationDate = registationDate;
        }
    }
}
