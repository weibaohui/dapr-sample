namespace pubsub
{
    public class Config
    {
        /// <summary>
        ///     State store name.
        /// </summary>
        public const string StoreName = "statestore";

        /// <summary>
        ///     Pubsub component name.  "pubsub" is name of the default pub/sub configured by the Dapr CLI.
        /// </summary>
        public const string PubsubName = "pubsub";

        public const string RemoteAppId = "pong";
        public const string RemoteAccountId = "999";
    }
}