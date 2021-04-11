namespace pubsub.module
{
    public class Transaction
    {
        /// <summary>
        ///     Gets or sets account id for the transaction.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets amount for the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"id:{Id},amount:{Amount}";
        }
    }
}