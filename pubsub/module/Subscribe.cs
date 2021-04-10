using System.Text.Json.Serialization;

namespace pubsub.module
{
    public class Subscribe
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pubsubname")]
        public string PubSubName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Topic { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Route { get; set; }
    }
}