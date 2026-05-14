using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Resources.Camera
{
    [DataContract]
    public class AddCameraViewModel
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public string CameraName { get; set; }
        [JsonIgnore]
        public double Battery { get; set; }
        [JsonIgnore]
        public string RealtimeCameraLink { get; set; }

        //Sensor
        [JsonIgnore]
        public AIDetectType AIDetection { get; set; }
        [JsonIgnore]
        public FlameSensorType FlameSensor { get; set; }
        [JsonIgnore]
        public double SmokeValue { get; set; }

        //Flag
        [JsonIgnore]
        public StateType CameraState { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; }
        [DataMember]
        [JsonIgnore]
        public DateTime RegistationDate { get; set; }

        public AddCameraViewModel(string id, string userId, string cameraName, double battery, string realtimeCameraLink, AIDetectType aIDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, DateTime timeStamp, DateTime registationDate)
        {
            Id = id;
            UserId = "NSX";
            CameraName = "AI Camera";
            Battery = battery;
            RealtimeCameraLink = "Not available";
            AIDetection = aIDetection;
            FlameSensor = flameSensor;
            SmokeValue = smokeValue;
            CameraState = cameraState;
            TimeStamp = timeStamp;
            RegistationDate = DateTime.Now;
        }
    }
}
