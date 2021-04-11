using Microsoft.AspNetCore.Mvc;
using pubsub.module;

namespace pubsub.Controllers
{
    /// <summary>
    ///     dotnet 常规使用
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class JsonController : ControllerBase
    {
        [HttpGet]
        [Route("/hello")]
        public CloudEvent HelloWorld()
        {
            var ce = new CloudEvent();
            var ceData = new AppSampleData {Message = "MSG", MessageType = "TYPE"};
            ce.AppSampleData = ceData;
            ce.DataContentType = "json";
            ce.Id = "id";
            ce.PubSubName = "pubsubname";

            return ce;
        }

        [HttpPost]
        [Route("/hello")]
        public CloudEvent DoHelloWorld(CloudEvent ce)
        {
            return ce;
        }

        [HttpPost]
        [Route("/hello/{msg}/{age:int}")]
        public string DoHelloWorld(string msg, int age)
        {
            return $"hello {msg} is {age} old";
        }
    }
}