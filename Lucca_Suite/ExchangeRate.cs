namespace Lucca_Suite
{
    public class ExchangeRate
    {
        public ExchangeRate(string sourceCurrencySymbol, double rate, string destinationCurrencySymbol)
        {
            SourceCurrencySymbol = sourceCurrencySymbol;
            Rate = rate;
            DestinationCurrencySymbol = destinationCurrencySymbol;  
        }

        public string SourceCurrencySymbol { get; private set; }
        public double Rate { get; private set; }
        public string DestinationCurrencySymbol { get; private set; }
    }
}
