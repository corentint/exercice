using Lucca_Suite.Model;
using System.Security;

namespace Lucca_Suite
{
    public class CurrencyConverter
    {
        const int NUMBER_OF_DECIMALS = 4;
        public CurrencyData CurrencyData { get; set; }

        public CurrencyConverter(CurrencyData currencyData)
        {
            CurrencyData = currencyData;
        }

        public decimal GetResult()
        {
            var exchangePath = BuildExchangePath(
                CurrencyData.ExchangeRates,
                CurrencyData.InitialMoney.Currency,
                CurrencyData.TargetCurrency);

            return ApplyExchangeRates(CurrencyData.InitialMoney.Amount, exchangePath);
        }

        public decimal BFSAlgo()
        {
            var vertices = CurrencyData.ExchangeRates.Select(x => x.SourceCurrencySymbol).Distinct();

            var edges = CurrencyData.ExchangeRates.Select(x => new Tuple<string, string>(x.SourceCurrencySymbol, x.DestinationCurrencySymbol));

            var graph = new Graph<string>(vertices, edges);

            var algortihm = new BFSAlgorithm();

            var shortestPathFunc = algortihm.ShortestPathFunction(graph, "EUR");

            var resultCurrencies = shortestPathFunc(CurrencyData.TargetCurrency);

            var resultExchangeRates = ConvertToExchangeRates(resultCurrencies.ToArray(), CurrencyData.ExchangeRates);

            return ApplyExchangeRates(CurrencyData.InitialMoney.Amount, resultExchangeRates);
        }

        private IEnumerable<ExchangeRate> ConvertToExchangeRates(
            string[] resultCurrencies,
            IEnumerable<ExchangeRate> allExchangeRates)
        {
            var exchangeRates = new List<ExchangeRate>();

            for (int i = 0; i < resultCurrencies.Count() - 1; i++)
            {
                exchangeRates.Add(allExchangeRates.Single(
                    x => x.SourceCurrencySymbol == resultCurrencies[i]
                        && x.DestinationCurrencySymbol == resultCurrencies[i + 1]));
            }

            return exchangeRates;
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
                    throw new Exception($"There is no exchange path between {initialCurrency} and {targetCurrency}");
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
                result = Math.Round(result * exchangeRate.Rate, NUMBER_OF_DECIMALS);
            }
            return Math.Ceiling(result);
        }
    }
}
