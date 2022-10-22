using System.Linq;

namespace Lucca_Suite
{
    internal class CurrencyConverter
    {
        public FileReader FileReader { get; set; }

        public CurrencyConverter(FileReader fileReader)
        {
            FileReader = fileReader;
        }

        public decimal GetResult()
        {
            var currencies = FileReader.ExchangeRates;
            var exchangePath = new Stack<ExchangeRate>();
            string currentlyTestedCurrency = FileReader.InitialMoney.Currency;

            Func<ExchangeRate, bool> exchangeRateWithMatchingSource = x => x.SourceCurrencySymbol == currentlyTestedCurrency
                                        && !exchangePath.Any(y => y.SourceCurrencySymbol == x.DestinationCurrencySymbol);

            for (int i = 0; i < 20; i++)
            {
                if (currentlyTestedCurrency == FileReader.TargetCurrency)
                {
                    break;
                }
                else if (currencies.Any(exchangeRateWithMatchingSource))
                {
                    var exchange = currencies.FirstOrDefault(exchangeRateWithMatchingSource);
                    exchangePath.Push(exchange);
                    currentlyTestedCurrency = exchange.DestinationCurrencySymbol;
                }
                else
                {
                    var deadendlink = exchangePath.Pop();
                    currentlyTestedCurrency = deadendlink.SourceCurrencySymbol;
                    currencies.Remove(deadendlink);
                }
            }

            return ApplyExchangeRates(FileReader.InitialMoney.Amount, exchangePath.Reverse());
        }

        private decimal ApplyExchangeRates(decimal initialValue, IEnumerable<ExchangeRate> exchangeRates)
        {
            decimal result = initialValue;
            foreach (var exchangeRate in exchangeRates)
            {
                result = Math.Round(result * exchangeRate.Rate, 4);
            }
            return Math.Ceiling(result);
        }
    }
}
