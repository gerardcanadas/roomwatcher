using roomwatcher.backend.comms.push.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace roomwatcher.backend.web.api.Controllers
{
    [RoutePrefix("api/ir")]
    public class IRController : ApiController
    {
        [HttpGet]
        [Route("")]
        public void RecordData(string value)
        {
            // send push notification
            AndroidManager push = new AndroidManager();
            push.SendPush(value);
        }
    }
}
