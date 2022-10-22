namespace Lucca_Suite
{
    public class Money
    {
        public string Currency { get; private set; }
        public decimal Amount { get; private set; }

        public Money(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }
    }
}
