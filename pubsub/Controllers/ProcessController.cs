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
        public async Task<OkObjectResult> ProcessA(CloudEvent ce)
        {
            Console.WriteLine(JsonSerializer.Serialize(ce));
            return Ok(ce);
        }

        [HttpPost]
        [Route("processTiny")]
        [Topic(Config.PubsubName, "B")]
        public async Task<OkObjectResult> ProcessTiny(AppSampleData appSampleData)
        {
            Console.WriteLine(JsonSerializer.Serialize(appSampleData));
            return Ok(appSampleData);
        }
    }
}