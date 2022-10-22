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
            List<string> excludedCurrencies = new List<string>();
            excludedCurrencies.Add(FileReader.InitialMoney.Currency);


            var matchingValues = FileReader.MatchingValues;

            var initialCurrenciesAvailable = matchingValues[FileReader.InitialMoney.Currency];

            // curerncies not to consider

            // Find path between node algorithm
            var currencies = FileReader.currencies;
            var stackResult = new Stack<Tuple<string, string>>();
            var initialCurrency = FileReader.InitialMoney.Currency;
            var targetCurrency = FileReader.TargetCurrency;

            string currentlyTestedCurrency = initialCurrency;
            for (int i = 0; i < 20; i++)
            {
                if (currentlyTestedCurrency == targetCurrency)
                {
                    break;
                }
                else if (currencies.Any(x => x.Item1 == currentlyTestedCurrency && !stackResult.Any(y => y.Item1 == x.Item2)))
                {
                    var currencyLink = currencies.FirstOrDefault(x => x.Item1 == currentlyTestedCurrency && !stackResult.Any(y => y.Item1 == x.Item2));
                    stackResult.Push(currencyLink);
                    currentlyTestedCurrency = currencyLink.Item2;
                }
                else
                {
                    var deadenlink = stackResult.Pop();
                    currentlyTestedCurrency = deadenlink.Item1;
                    currencies.Remove(deadenlink);
                }
            }

            return 0;
        }

        public void FindConversionPath()
        {


            //var allCurrencies = ExchangeRates.Select(x => x.SourceCurrencySymbol).Concat(ExchangeRates.Select(x => x.SourceCurrencySymbol)).Distinct().ToList();

            //var test = ExchangeRates
            //    .Where(y => y.SourceCurrencySymbol == x || y.DestinationCurrencySymbol == x)
            //    .Select(y => y.SourceCurrencySymbol)
            //    .ToList();
            //Dictionary<string, List<string>> availableExchanges =
            //    allCurrencies.ToDictionary(x => new List<string>());

            // For one of the 2 values we want to go towards the target currency
        }
    }
}
