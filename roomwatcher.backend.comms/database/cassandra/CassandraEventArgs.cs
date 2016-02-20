using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using roomwatcher.bo.sensors.ir;
using roomwatcher.bo.sensors.interfaces;

namespace roomwatcher.backend.comms.database.cassandra
{
    public class CassandraEventArgs : EventArgs
    {
        private ISensor SensorData;
        public CassandraEventArgs(ISensor sensorData)
        {
            this.SensorData = sensorData;
        }


        public ISensor GetSensorData()
        {
            return this.SensorData;
        }

        //public string GetEventoDTOString()
        //{
        //    return this.EventDTO.ToString();
        //}
    }
}
