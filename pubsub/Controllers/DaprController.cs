using System;
using Microsoft.AspNetCore.Mvc;

namespace pubsub.Controllers
{
    [ApiController]
    [Route("dapr")]
    public class DaprController : ControllerBase
    {
        [HttpGet]
        [Route("subscribe")]
        public OkObjectResult subscribe()
        {
            var sub = new[]
            {
                new Subscribe
                {
                    topic = "A",
                    pubsubName = "pubsub",
                    route = "/Process/processA"
                },
                new Subscribe
                {
                    topic = "A",
                    pubsubName = "pubsub",
                    route = "/Process/processTiny"
                }
            };
            Console.WriteLine("dapr 已经读取订阅信息");
            return Ok(sub);
        }
    }
}