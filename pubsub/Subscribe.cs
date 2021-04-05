using System.Text.Json.Serialization;

namespace pubsub
{
    public class Subscribe
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pubsubname")]
        public string pubsubName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string route { get; set; }
    }
}