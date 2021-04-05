
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace pubsub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        [HttpPost]
        [Route("processA")]
        public async Task<OkObjectResult> ProcessA(CloudEvent ce)
        {
            Console.WriteLine(JsonSerializer.Serialize(ce));
            return Ok(ce);
        }
        [HttpPost]
        [Route("processTiny")]
        public async Task<OkObjectResult> ProcessTiny(TinyCloudEvent ce)
        {
            Console.WriteLine(JsonSerializer.Serialize(ce.data));
            return Ok(ce);
        }
    }
}