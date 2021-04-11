using Microsoft.AspNetCore.Mvc;

namespace pubsub.Controllers
{
    /// <summary>
    ///     HTTP方式使用Dapr PubSub功能
    /// </summary>
    [ApiController]
    [Route("dapr")]
    public class DaprController : ControllerBase
    {
        // [HttpGet]
        // [Route("subscribe")]
        // public OkObjectResult subscribe()
        // {
        //     var sub = new[]
        //     {
        //         new Subscribe
        //         {
        //             Topic = "A",
        //             PubSubName = "pubsub",
        //             Route = "/Process/processA"
        //         },
        //         new Subscribe
        //         {
        //             Topic = "B",
        //             PubSubName = "pubsub",
        //             Route = "/Process/processTiny"
        //         }
        //     };
        //     Console.WriteLine("dapr 已经读取订阅信息");
        //     return Ok(sub);
        // }
    }
}