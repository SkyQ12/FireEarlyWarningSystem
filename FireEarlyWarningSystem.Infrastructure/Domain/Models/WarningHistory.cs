using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Models
{
    public class WarningHistory
    {
        public int Id { get; set; }
        public string CameraId { get; set; }
        public Camera Camera { get; set; }
        //Sensor
        public AIDetectType AIDetection { get; set; }
        public FlameSensorType FlameSensor { get; set; }
        public double SmokeValue { get; set; }
        //Flag
        public StateType CameraState { get; set; }
        public DateTime WarningTime { get; set; }

        public WarningHistory()
        {
        }

        public WarningHistory(int id, string cameraId, Camera camera, AIDetectType aIDetection, FlameSensorType flameSensor, double smokeValue, StateType cameraState, DateTime warningTime)
        {
            Id = id;
            CameraId = cameraId;
            Camera = camera;
            AIDetection = aIDetection;
            FlameSensor = flameSensor;
            SmokeValue = smokeValue;
            CameraState = cameraState;
            WarningTime = warningTime;
        }
    }
}
