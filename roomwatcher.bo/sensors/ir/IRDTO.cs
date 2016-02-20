using roomwatcher.bo.sensors.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roomwatcher.bo.sensors.ir
{
    public class IRDTO : ISensor
    {
        public string Id { get; set; }
        public string SensorId { get; set; }
        public string Datetime { get; set; }
        public string Value { get; set; }

        public IRDTO()
        {

        }

        public IRDTO(string sensorId, string value)
        {
            this.SensorId = sensorId;
            this.Datetime = DateTime.Now.ToString();
            this.Value = value;
        }
    }
}
