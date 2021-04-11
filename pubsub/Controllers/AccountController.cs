using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pubsub.module;

namespace pubsub.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;

        private readonly DaprClient client;

        public AccountController(ILogger<AccountController> logger, DaprClient client)
        {
            this.logger = logger;
            this.client = client;
        }


        /// <summary>
        ///     或者指定账户ID的账户信息
        /// </summary>
        /// <param name="account">Account information for the id from Dapr state store.</param>
        /// <returns>Account information.</returns>
        [HttpGet("{account}")]
        public ActionResult<Account> Get([FromState(Config.StoreName)] StateEntry<Account> account)
        {
            logger.LogInformation($"Information获取ID为{account.Key},{account.Value},{account.ETag}的账户");
            if (account.Value is null) return NotFound();

            return account.Value;
        }

        /// <summary>
        ///     监听存钱的消息
        ///     目前监听了两种方式，1是httpPost，2是消息事件deposit
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        [Topic(Config.PubsubName, "deposit")]
        [HttpPost("deposit")]
        public async Task<ActionResult<Account>> Deposit(Transaction transaction)
        {
            logger.LogDebug($"Enter deposit-{transaction}");
            var state = await client.GetStateEntryAsync<Account>(Config.StoreName, transaction.Id);
            state.Value ??= new Account {Id = transaction.Id};
            state.Value.Balance += transaction.Amount;
            await state.SaveAsync();
            return state.Value;
        }

        /// <summary>
        ///     取钱
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        [Topic(Config.PubsubName, "withdraw")]
        [HttpPost("withdraw")]
        public async Task<ActionResult<Account>> Withdraw(Transaction transaction)
        {
            logger.LogDebug($"Enter withdraw-{transaction}");
            var state = await client.GetStateEntryAsync<Account>(Config.StoreName, transaction.Id);

            if (state.Value == null) return NotFound();

            state.Value.Balance -= transaction.Amount;
            await state.SaveAsync();
            return state.Value;
        }
    }
}