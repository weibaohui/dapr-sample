using System;
using Microsoft.AspNetCore.Mvc;
using pubsub.module;

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
                    Topic = "A",
                    PubSubName = "pubsub",
                    Route = "/Process/processA"
                },
                new Subscribe
                {
                    Topic = "A",
                    PubSubName = "pubsub",
                    Route = "/Process/processTiny"
                }
            };
            Console.WriteLine("dapr 已经读取订阅信息");
            return Ok(sub);
        }
    }
}