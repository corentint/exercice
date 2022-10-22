using System.Globalization;

namespace Lucca_Suite
{
    public class FileReader
    {
        private readonly string[] _lines;

        public List<ExchangeRate> ExchangeRates { get; set; }
        public Money InitialMoney { get; set; }
        public string TargetCurrency { get; set; }

        public FileReader(string filePath)
        {
            _lines = File.ReadAllLines(filePath);
            ExchangeRates = BuildExchangeRates(_lines);
            ImportFirstLine();
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

            for (int i = 2; i < lines.Length; i++)
            {
                var lineParts = lines[i].Split(";");

                exchangeRates.Add(new ExchangeRate(lineParts[0], lineParts[1], decimal.Parse(lineParts[2], CultureInfo.InvariantCulture)));
                exchangeRates.Add(new ExchangeRate(lineParts[1], lineParts[0], 1 / decimal.Parse(lineParts[2], CultureInfo.InvariantCulture)));
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
    }
}