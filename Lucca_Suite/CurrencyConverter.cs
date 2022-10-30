using Lucca_Suite.Algorithm;
using Lucca_Suite.Interface;
using Lucca_Suite.Model;

namespace Lucca_Suite
{
    public class CurrencyConverter
    {
        const int NUMBER_OF_DECIMALS = 4;

        private readonly IShortestPathFinder _algorithm;

        public CurrencyConverter(IShortestPathFinder algorithm)
        {
            _algorithm = algorithm;
        }

        public decimal GetResult(CurrencyData currencyData)
        {
            var vertices = currencyData.ExchangeRates.Select(x => x.SourceCurrencySymbol).Distinct();

            var edges = currencyData.ExchangeRates.Select(x => new Tuple<string, string>(x.SourceCurrencySymbol, x.DestinationCurrencySymbol));

            var graph = new Graph<string>(vertices, edges);

            var resultCurrencies = _algorithm.GetShortestPath(graph, currencyData.InitialMoney.Currency, currencyData.TargetCurrency);

            var resultExchangeRates = ConvertPathToExchangeRates(resultCurrencies.ToArray(), currencyData.ExchangeRates);

            return ApplyExchangeRates(currencyData.InitialMoney.Amount, resultExchangeRates);
        }

        private static IEnumerable<ExchangeRate> ConvertPathToExchangeRates(
            string[] resultCurrencies,
            List<ExchangeRate> allExchangeRates)
        {
            var exchangeRates = new List<ExchangeRate>();

            for (int i = 0; i < resultCurrencies.Length - 1; i++)
            {
                exchangeRates.Add(allExchangeRates.Single(
                    x => x.SourceCurrencySymbol == resultCurrencies[i]
                        && x.DestinationCurrencySymbol == resultCurrencies[i + 1]));
            }

            return exchangeRates;
        }

        private static decimal ApplyExchangeRates(decimal initialValue, IEnumerable<ExchangeRate> exchangeRates)
        {
            decimal result = initialValue;
            foreach (var exchangeRate in exchangeRates)
            {
                result = Math.Round(result * Math.Round(exchangeRate.Rate, NUMBER_OF_DECIMALS), NUMBER_OF_DECIMALS);
            }
            return Math.Ceiling(result);
        }
    }
}
