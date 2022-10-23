using System.Globalization;

namespace Lucca_Suite
{
    public class StringToObjectsAdapter
    {
        const string DATA_SEPARATOR = ";";
        public FileReader FileReader { get; }
        public List<ExchangeRate> ExchangeRates { get; set; }
        public Money InitialMoney { get; set; }
        public string TargetCurrency { get; set; }

        public StringToObjectsAdapter(FileReader fileReader)
        {
            FileReader = fileReader;
            ExchangeRates = BuildExchangeRates(FileReader.Lines);
            ImportFirstLine();
        }

        private List<ExchangeRate> BuildExchangeRates(string[] lines)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

            for (int i = 2; i < lines.Length; i++)
            {
                var lineParts = lines[i].Split(DATA_SEPARATOR);

                exchangeRates.Add(
                    new ExchangeRate(
                        GetSourceCurrency(lineParts),
                        GetDestinationCurrency(lineParts),
                        GetExchangeRateValue(lineParts)));
                exchangeRates.Add(
                    new ExchangeRate(GetDestinationCurrency(lineParts),
                    GetSourceCurrency(lineParts),
                    1 / GetExchangeRateValue(lineParts)));
            }
            return exchangeRates;
        }

        private void ImportFirstLine()
        {
            InitialMoney = BuildInitialMoney(BuildFirstLineParts());
            TargetCurrency = BuildTargetCurrency(BuildFirstLineParts());
        }

        private string[] BuildFirstLineParts()
        {
            return FileReader.Lines[0].Split(DATA_SEPARATOR);
        }

        private string GetSourceCurrency(string[] lineParts)
        {
            return lineParts[0];
        }

        private decimal GetExchangeRateValue(string[] lineParts)
        {
            return decimal.Parse(lineParts[2], CultureInfo.InvariantCulture);
        }

        private string GetDestinationCurrency(string[] lineParts)
        {
            return lineParts[1];
        }

        private Money BuildInitialMoney(string[] firstLineParts)
        {
            return new Money(
                BuildInitialCurrency(firstLineParts),
                BuildInitialAmount(firstLineParts));
        }

        private string? BuildInitialCurrency(string[] lineParts)
        {
            return lineParts[0];
        }

        private decimal BuildInitialAmount(string[] lineParts)
        {
            return decimal.Parse(lineParts[1]);
        }

        private string? BuildTargetCurrency(string[] lineParts)
        {
            return lineParts[2];
        }
    }
}
