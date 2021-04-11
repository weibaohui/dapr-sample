using System.Net.Http.Json;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pubsub.module;

namespace pubsub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvokeServiceHttp : ControllerBase
    {
        private readonly ILogger<InvokeServiceHttp> _logger;

        public InvokeServiceHttp(ILogger<InvokeServiceHttp> logger)
        {
            _logger = logger;
        }


        /// <summary>
        ///     调用远程的APPID方法
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="accountId"></param>
        [HttpGet]
        [Route("[action]")]
        public async Task<Account> InvokeAppId(string appId = Config.RemoteAppId,
            string accountId = Config.RemoteAccountId)
        {
            using var client = new DaprClientBuilder().Build();
            _logger.LogInformation($"获得一个dapr client {client}");
            _logger.LogInformation("调用存钱方法");
            var data = new Transaction {Id = accountId, Amount = 99};
            var account = await client.InvokeMethodAsync<Transaction, Account>(appId, "deposit", data);
            _logger.LogInformation($"调用返回{account}");
            return account;
        }

        /// <summary>
        ///     调用远程的APPID方法
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<Account> InvokeAppIdByHttp(string appId = Config.RemoteAppId,
            string accountId = Config.RemoteAccountId)
        {
            var client = DaprClient.CreateInvokeHttpClient(appId);

            var deposit = new Transaction {Id = accountId, Amount = 1};
            var response = await client.PostAsJsonAsync("/deposit", deposit);
            var account = await response.Content.ReadFromJsonAsync<Account>();
            _logger.LogInformation($"调用返回{account}");
            return account;
        }
    }
}