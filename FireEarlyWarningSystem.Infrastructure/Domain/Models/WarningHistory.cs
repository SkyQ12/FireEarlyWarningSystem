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
        public YoloDetectType YoloDetection { get; set; }
        public double TemperatureValue { get; set; }
        public double SmokeValue { get; set; }
        //Flag
        public State CameraState { get; set; }
        public DateTime WarningTime { get; set; }

        public WarningHistory()
        {
        }

        public WarningHistory(int id, string cameraId, Camera camera, YoloDetectType yoloDetection, double temperatureValue, double smokeValue, State cameraState, DateTime warningTime)
        {
            Id = id;
            CameraId = cameraId;
            Camera = camera;
            YoloDetection = yoloDetection;
            TemperatureValue = temperatureValue;
            SmokeValue = smokeValue;
            CameraState = cameraState;
            WarningTime = warningTime;
        }
    }
}
