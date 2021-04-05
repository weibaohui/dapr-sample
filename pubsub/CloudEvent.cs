namespace pubsub
{
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string messageType { get; set; }
        /// <summary>
        /// 柔柔弱弱
        /// </summary>
        public string message { get; set; }
    }
    public class CloudEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string pubsubname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string traceid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string datacontenttype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string topic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string specversion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Data data { get; set; }
        
    }
}