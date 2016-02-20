using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace roomwatcher.bo.sensors.interfaces
{
    public interface ISensor
    {
        string Id { get; set; }
        string SensorId { get; set; }
        string DateTime { get; set; }
        string Value { get; set; }
    }
}
