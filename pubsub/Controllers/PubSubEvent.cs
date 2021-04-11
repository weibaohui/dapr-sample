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


        /// <summary>
        ///     所有监听此消息的服务，都会增加相应的钱数
        /// </summary>
        /// <param name="client"></param>
        [HttpGet]
        [Route("[action]")]
        public async void PubDepositEvent([FromServices] DaprClient client)
        {
            _logger.LogWarning($"给{Config.RemoteAccountId}增加1000000");
            var eventData = new {Id = Config.RemoteAccountId, Amount = 1000000};
            await client.PublishEventAsync(Config.PubsubName, "deposit", eventData);
        }

        [HttpGet]
        [Route("[action]")]
        public async void PubWithDrawEvent([FromServices] DaprClient client)
        {
            _logger.LogWarning($"给{Config.RemoteAccountId}取出1");
            var eventData = new {Id = Config.RemoteAccountId, Amount = 1};
            await client.PublishEventAsync(Config.PubsubName, "withdraw", eventData);
        }
    }
}