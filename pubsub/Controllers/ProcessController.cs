using System;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using pubsub.module;

namespace pubsub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        [HttpPost]
        [Route("processA")]
        [Topic(Config.PubsubName, "A")]
        public OkObjectResult ProcessA(CloudEvent ce)
        {
            Console.WriteLine(JsonSerializer.Serialize(ce));
            return Ok(ce);
        }

        [HttpPost]
        [Route("processTiny")]
        [Topic(Config.PubsubName, "B")]
        public OkObjectResult ProcessTiny(AppSampleData appSampleData)
        {
            Console.WriteLine(JsonSerializer.Serialize(appSampleData));
            return Ok(appSampleData);

        }

        [HttpGet("act")]
        [Topic(Config.PubsubName, "C")]
        public void ProcessAct()
        {
            Console.WriteLine($"ddddd44");
        }

        [HttpPost("/rrr")]
        public string ProcessAct2()
        {
            return "sss";
        }
    }
}