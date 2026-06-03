using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.MqttClients
{
    public class CameraMetric
    {
        public string CameraId { get; set; }

        public int AiDetection { get; set; }

        public int FlameSensor { get; set; }

        public double SmokeValue { get; set; }

        public int CameraState { get; set; }
        public double Battery { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
