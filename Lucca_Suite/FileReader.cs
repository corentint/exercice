using System.Globalization;

namespace Lucca_Suite
{
    public class FileReader
    {
        private readonly string[] _lines;

        public List<ExchangeRate> ExchangeRates { get; set; }

        public Money InitialMoney { get; set; }
        public string TargetCurrency { get; set; }

        public Dictionary<string, List<string>> MatchingValues { get; private set; }
        public List<Tuple<string, string>> currencies {get;set;}

        public FileReader(string filePath)
        {
            _lines = File.ReadAllLines(filePath);
            ExchangeRates = BuildExchangeRates(_lines);

            ImportFirstLine();

            this.DisplayData();
        }

        private void ImportFirstLine()
        {
            InitialMoney = BuildInitialMoney(BuildFirstLineParts());
            TargetCurrency = BuildTargetCurrency(BuildFirstLineParts());
        }

        private string[] BuildFirstLineParts()
        {
            return _lines[0].Split(";");
        }

        private List<ExchangeRate> BuildExchangeRates(string[] lines)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
            MatchingValues = new Dictionary<string, List<string>>();
            currencies = new List<Tuple<string, string>>();

            for (int i = 2; i < lines.Length; i++)
            {
                var lineParts = lines[i].Split(";");
                exchangeRates.Add(new ExchangeRate(lineParts[0], lineParts[1], double.Parse(lineParts[2], CultureInfo.InvariantCulture)));

                if (MatchingValues.ContainsKey(lineParts[0]))
                {
                    MatchingValues[lineParts[0]].Add(lineParts[1]);
                }
                else if (MatchingValues.ContainsKey(lineParts[1]))
                {
                    MatchingValues[lineParts[1]].Add(lineParts[0]);
                }
                else
                {
                    MatchingValues.Add(lineParts[0], new List<string>() { lineParts[1] });
                    MatchingValues.Add(lineParts[1], new List<string>() { lineParts[0] });
                }

                currencies.Add(new Tuple<string, string>(lineParts[0], lineParts[1]));
                currencies.Add(new Tuple<string, string>(lineParts[1], lineParts[0]));
            }
            return exchangeRates;
        }

        private Money BuildInitialMoney(string[] firstLineParts)
        {
            return new Money(
                BuildInitialCurrency(firstLineParts),
                BuildInitialAmount(firstLineParts));
        }

        private string? BuildInitialCurrency(string[] firstLineParts)
        {
            return firstLineParts[0];
        }

        private decimal BuildInitialAmount(string[] firstLineParts)
        {
            return decimal.Parse(firstLineParts[1]);
        }

        private string? BuildTargetCurrency(string[] firstLineParts)
        {
            return firstLineParts[2];
        }

        private void DisplayData()
        {
            foreach (ExchangeRate exchangeRate in ExchangeRates)
            {
                Console.WriteLine($"we can convert : {exchangeRate.SourceCurrencySymbol} to {exchangeRate.DestinationCurrencySymbol} with a rate of : {exchangeRate.Rate}");
            }

            Console.WriteLine($"initial amount : {InitialMoney.Amount} in {InitialMoney.Currency} into {TargetCurrency}");
        }
    }
}