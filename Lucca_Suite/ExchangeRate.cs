namespace Lucca_Suite
{
    public class ExchangeRate
    {
        public ExchangeRate(string sourceCurrencySymbol, string destinationCurrencySymbol, double rate)
        {
            SourceCurrencySymbol = sourceCurrencySymbol;
            DestinationCurrencySymbol = destinationCurrencySymbol;  
            Rate = rate;
        }

        public string SourceCurrencySymbol { get; private set; }
        public string DestinationCurrencySymbol { get; private set; }
        public double Rate { get; private set; }
    }
}
