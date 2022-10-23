using System.Linq;

namespace Lucca_Suite
{
    internal class CurrencyConverter
    {
        public StringToObjectsAdapter StringToObjectsAdapter { get; set; }

        public CurrencyConverter(StringToObjectsAdapter stringToObjectsAdapter)
        {
            StringToObjectsAdapter = stringToObjectsAdapter;
        }

        public decimal GetResult()
        {
            var exchangePath = BuildExchangePath(
                StringToObjectsAdapter.ExchangeRates,
                StringToObjectsAdapter.InitialMoney.Currency,
                StringToObjectsAdapter.TargetCurrency);

            return ApplyExchangeRates(StringToObjectsAdapter.InitialMoney.Amount, exchangePath);
        }

        private IEnumerable<ExchangeRate> BuildExchangePath(
            List<ExchangeRate> availableEchangeRates,
            string initialCurrency,
            string targetCurrency)
        {
            var exchangeRatesPath = new List<ExchangeRate>();
            string currentlyTestedCurrency = initialCurrency;

            Func<ExchangeRate, bool> exchangeRateWithMatchingSource = 
                x => 
                x.SourceCurrencySymbol == currentlyTestedCurrency
                && !exchangeRatesPath.Any(y => y.SourceCurrencySymbol == x.DestinationCurrencySymbol);

            while(true)
            { 
                if (currentlyTestedCurrency == targetCurrency)
                {
                    // We found an exchange path
                    break;
                }
                else if (availableEchangeRates.Any(exchangeRateWithMatchingSource))
                {
                    // The currently tested currency may lead to the target currency
                    currentlyTestedCurrency = AddExchangeRateToPath(
                        exchangeRatesPath,
                        availableEchangeRates,
                        exchangeRateWithMatchingSource);
                }
                else if (exchangeRatesPath.Count == 0)
                {
                    // An Exchange path does not exist
                    Console.WriteLine("not possible!");
                    break;
                }
                else
                {
                    // The currently tested currency does not lead to the target currency
                    currentlyTestedCurrency = RemoveExchangeRateFromPath(
                        exchangeRatesPath,
                        availableEchangeRates);
                }
            }

            return exchangeRatesPath;
        }

        private string AddExchangeRateToPath(
            List<ExchangeRate> exchangeRatesPath,
            List<ExchangeRate> availableEchangeRates,
            Func<ExchangeRate, bool> exchangeRateWithMatchingSource)
        {
            var exchange = availableEchangeRates.FirstOrDefault(exchangeRateWithMatchingSource);
            exchangeRatesPath.Add(exchange);
            return exchange.DestinationCurrencySymbol;
        }

        private string RemoveExchangeRateFromPath(
            List<ExchangeRate> exchangeRatesPath,
            List<ExchangeRate> availableEchangeRates)
        {
            var deadendlink = exchangeRatesPath[exchangeRatesPath.Count - 1];
            exchangeRatesPath.Remove(deadendlink);
            availableEchangeRates.Remove(deadendlink);

            return deadendlink.SourceCurrencySymbol;
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
