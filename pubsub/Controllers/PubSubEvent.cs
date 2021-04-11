using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pubsub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubSubEvent : ControllerBase
    {
        private readonly ILogger<PubSubEvent> _logger;

        public PubSubEvent(ILogger<PubSubEvent> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public async void PubDepositEvent([FromServices] DaprClient client)
        {
            var eventData = new {Id = "177", Amount = 1000000};
            await client.PublishEventAsync(Config.PubsubName, "deposit", eventData);
        }

        [HttpGet]
        [Route("[action]")]
        public async void PubWithDrawEvent([FromServices] DaprClient client)
        {
            var eventData = new {Id = "177", Amount = 1};
            await client.PublishEventAsync(Config.PubsubName, "withdraw", eventData);
        }
    }
}