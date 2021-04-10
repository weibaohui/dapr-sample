using System;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using pubsub.module;

namespace pubsub.Controllers
{
    public class AccountController
    {
        public async Task Balance(HttpContext context)
        {
            Console.WriteLine("Enter Balance");
            var client = context.RequestServices.GetRequiredService<DaprClient>();

            var id = (string) context.Request.RouteValues["id"];
            Console.WriteLine("id is {0}", id);
            var account = await client.GetStateAsync<Account>(Config.StoreName, id);
            if (account == null)
            {
                Console.WriteLine("Account not found");
                context.Response.StatusCode = 404;
                return;
            }

            Console.WriteLine("Account balance is {0}", account.Balance);

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, account);
        }

        public async Task Deposit(HttpContext context)
        {
            Console.WriteLine("Enter Deposit");

            var client = context.RequestServices.GetRequiredService<DaprClient>();

            var transaction = await JsonSerializer.DeserializeAsync<Transaction>(context.Request.Body);
            Console.WriteLine("Id is {0}, Amount is {1}", transaction.Id, transaction.Amount);
            var account = await client.GetStateAsync<Account>(Config.StoreName, transaction.Id);
            if (account == null) account = new Account {Id = transaction.Id};

            if (transaction.Amount < 0m)
            {
                Console.WriteLine("Invalid amount");
                context.Response.StatusCode = 400;
                return;
            }

            account.Balance += transaction.Amount;
            await client.SaveStateAsync(Config.StoreName, transaction.Id, account);
            Console.WriteLine("Balance is {0}", account.Balance);

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, account);
        }

        public async Task Withdraw(HttpContext context)
        {
            Console.WriteLine("Enter Withdraw");
            var client = context.RequestServices.GetRequiredService<DaprClient>();
            var transaction = await JsonSerializer.DeserializeAsync<Transaction>(context.Request.Body);
            Console.WriteLine("Id is {0}", transaction.Id);
            var account = await client.GetStateAsync<Account>(Config.StoreName, transaction.Id);
            if (account == null)
            {
                Console.WriteLine("Account not found");
                context.Response.StatusCode = 404;
                return;
            }

            if (transaction.Amount < 0m)
            {
                Console.WriteLine("Invalid amount");
                context.Response.StatusCode = 400;
                return;
            }

            account.Balance -= transaction.Amount;
            await client.SaveStateAsync(Config.StoreName, transaction.Id, account);
            Console.WriteLine("Balance is {0}", account.Balance);

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, account);
        }
    }
}