using pubsub.module;

namespace pubsub
{
    public class CloudEvent
    {
        /// <summary>
        /// </summary>
        public string PubSubName { get; set; }

        /// <summary>
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        public string DataContentType { get; set; }

        /// <summary>
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// </summary>
        public string SpecVersion { get; set; }

        /// <summary>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// </summary>
        public AppSampleData AppSampleData { get; set; }
    }
}