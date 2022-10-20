using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Lucca_Suite
{
    public class FileReader
    {
        private readonly string[] _lines;

        public List<ExchangeRate> ExchangeRates { get; set; }

        public string InitialCurrency { get; set; }
        public decimal InitialAmount { get; set; }
        public string TargetCurrency { get; set; }

        public FileReader(string filePath)
        {
            _lines = File.ReadAllLines(filePath);
            ExchangeRates = BuildExchangeRates(_lines);

            var firstLineParts = _lines[0].Split(";");
            InitialCurrency = BuildInitialCurrency(_lines);
            InitialAmount = BuildInitialAmount(_lines);
            TargetCurrency = BuildTargetCurrency(_lines);
        }

        private List<ExchangeRate> BuildExchangeRates(string[] lines)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
            for (int i = 2; i < lines.Length; i++)
            {
                var lineParts = lines[i].Split(";");
                exchangeRates.Add(new ExchangeRate(lineParts[0], lineParts[1], double.Parse(lineParts[2], CultureInfo.InvariantCulture)));
            }
            return exchangeRates;
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
