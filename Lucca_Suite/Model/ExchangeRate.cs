namespace Lucca_Suite.Model
{
    public class ExchangeRate
    {
        public ExchangeRate(string sourceCurrencySymbol, string destinationCurrencySymbol, decimal rate)
        {
            SourceCurrencySymbol = sourceCurrencySymbol;
            DestinationCurrencySymbol = destinationCurrencySymbol;
            Rate = rate;
        }

        public string SourceCurrencySymbol { get; private set; }
        public string DestinationCurrencySymbol { get; private set; }
        public decimal Rate { get; private set; }
    }
}
