using Newtonsoft.Json;
using roomwatcher.backend.comms.database.cassandra;
using roomwatcher.backend.comms.push.android;
using roomwatcher.bo.sensors.ir;
using System.Web.Http;

namespace roomwatcher.backend.web.api.Controllers
{
    [RoutePrefix("api/ir")]
    public class IRController : ApiController
    {
        private AndroidManager push;
        private CassandraConnector cassandra;

        private IRController()
        {
            push = new AndroidManager();
            cassandra = new CassandraConnector();
        }

        [HttpGet]
        [Route("")]
        public void RecordData(string sensorId, string value)
        {
            IRDTO irData = new IRDTO(sensorId, value);
            // send push notification
            AndroidManager push = new AndroidManager();
            push.SendPush(JsonConvert.SerializeObject(irData));

            // insert data into cassandra
            cassandra.InsertSensorData(irData);
        }
    }
}
