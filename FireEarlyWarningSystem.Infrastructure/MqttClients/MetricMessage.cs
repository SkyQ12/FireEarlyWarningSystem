using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEarlyWarningSystem.Infrastructure.MqttClients
{
    public class MetricMessage
    {
        public object Value { get; set; }

        public MetricMessage(object value)
        {
            Value = value;
        }
    }
}
