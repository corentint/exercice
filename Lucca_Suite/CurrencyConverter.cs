using Lucca_Suite.Algorithm;
using Lucca_Suite.Interface;
using Lucca_Suite.Model;

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
            var vertices = CurrencyData.ExchangeRates.Select(x => x.SourceCurrencySymbol).Distinct();

            var edges = CurrencyData.ExchangeRates.Select(x => new Tuple<string, string>(x.SourceCurrencySymbol, x.DestinationCurrencySymbol));

            var graph = new Graph<string>(vertices, edges);

            IShortestPathFinder algorithm = new BFSAlgorithm();

            var resultCurrencies = algorithm.GetShortestPath(graph, CurrencyData.InitialMoney.Currency, CurrencyData.TargetCurrency);

            var resultExchangeRates = ConvertToExchangeRates(resultCurrencies.ToArray(), CurrencyData.ExchangeRates);

            return ApplyExchangeRates(CurrencyData.InitialMoney.Amount, resultExchangeRates);
        }

        private static IEnumerable<ExchangeRate> ConvertToExchangeRates(
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
