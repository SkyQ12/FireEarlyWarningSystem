using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera
{
    public class UpdateCameraViewModel
    {
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

        public UpdateCameraViewModel(string userId, string cameraName, double battery, string realtimeCameraLink, AIDetectType aIDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, DateTime timeStamp)
        {
            UserId = userId;
            CameraName = cameraName;
            Battery = battery;
            RealtimeCameraLink = realtimeCameraLink;
            AIDetection = aIDetection;
            FlameSensor = flameSensor;
            SmokeValue = smokeValue;
            CameraState = cameraState;
            TimeStamp = timeStamp;
        }
    }
}
