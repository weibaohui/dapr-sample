using System;
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


        public AccountController(ILogger<AccountController> logger)
        {
            this.logger = logger;
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
        /// <param name="daprClient"></param>
        /// <returns></returns>
        [Topic(Config.PubsubName, "deposit")]
        [HttpPost("deposit")]
        public async Task<ActionResult<Account>> Deposit(Transaction transaction, [FromServices] DaprClient daprClient)
        {
            logger.LogDebug($"Enter deposit-{transaction}");
            var state = await daprClient.GetStateEntryAsync<Account>(Config.StoreName, transaction.Id);
            state.Value ??= new Account {Id = transaction.Id};
            state.Value.Balance += transaction.Amount;
            await state.SaveAsync();
            return state.Value;
        }

        /// <summary>
        ///     Method for withdrawing from account as specified in transaction.
        /// </summary>
        /// <param name="transaction">Transaction info.</param>
        /// <param name="daprClient">State client to interact with Dapr runtime.</param>
        /// <returns>A <see cref="Task{TResult}" /> representing the result of the asynchronous operation.</returns>
        /// "pubsub", the first parameter into the Topic attribute, is name of the default pub/sub configured by the Dapr CLI.
        [Topic(Config.PubsubName, "withdraw")]
        [HttpPost("withdraw")]
        public async Task<ActionResult<Account>> Withdraw(Transaction transaction, [FromServices] DaprClient daprClient)
        {
            logger.LogDebug("Enter withdraw");
            var state = await daprClient.GetStateEntryAsync<Account>(Config.StoreName, transaction.Id);

            if (state.Value == null) return NotFound();

            state.Value.Balance -= transaction.Amount;
            await state.SaveAsync();
            return state.Value;
        }

        /// <summary>
        ///     Method for returning a BadRequest result which will cause Dapr sidecar to throw an RpcException
        [HttpPost("throwException")]
        public async Task<ActionResult<Account>> ThrowException(Transaction transaction,
            [FromServices] DaprClient daprClient)
        {
            Console.WriteLine("Enter ThrowException");
            var task = Task.Delay(10);
            await task;
            return BadRequest(new {statusCode = 400, message = "bad request"});
        }
    }
}