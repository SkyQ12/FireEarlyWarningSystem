using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.Domain.Resources.WarningHistory
{
    public class WariningHistoryViewModel
    {
        public int Id { get; set; }
        public string CameraId { get; set; }
        public AIDetectType AIDetection { get; set; }
        public FlameSensorType FlameSensor { get; set; }
        public double SmokeValue { get; set; }
        //Flag
        public StateType CameraState { get; set; }
        public DateTime WarningTime { get; set; }

    }
}
