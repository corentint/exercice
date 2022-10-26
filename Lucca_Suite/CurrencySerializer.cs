using System.Globalization;
using Lucca_Suite.Model;

namespace Lucca_Suite
{
    public class CurrencySerializer
    {
        const string DATA_SEPARATOR = ";";

        public static CurrencyData Deserialize(string[] lines)
        {
            var firstLine = BuildFirstLineParts(lines);
            var result = new CurrencyData
            {
                ExchangeRates = BuildExchangeRates(lines),
                InitialMoney = BuildInitialMoney(firstLine),
                TargetCurrency = BuildTargetCurrency(firstLine),
            };

            return result;
        }

        private static List<ExchangeRate> BuildExchangeRates(string[] lines)
        {
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();

            for (int i = 2; i < lines.Length; i++)
            {
                var lineParts = lines[i].Split(DATA_SEPARATOR);

                var sourceCurrency = GetSourceCurrency(lineParts);
                var destinationCurrency = GetDestinationCurrency(lineParts);
                var exchangeRateValue = GetExchangeRateValue(lineParts);

                exchangeRates.Add(
                    new ExchangeRate(
                        sourceCurrency,
                        destinationCurrency,
                        exchangeRateValue));
                exchangeRates.Add(
                    new ExchangeRate(
                        destinationCurrency,
                        sourceCurrency,
                        1 / exchangeRateValue));
            }
            return exchangeRates;
        }

        private static string[] BuildFirstLineParts(string[] lines)
        {
            return lines[0].Split(DATA_SEPARATOR);
        }

        private static string GetSourceCurrency(string[] lineParts)
        {
            return lineParts[0];
        }

        private static decimal GetExchangeRateValue(string[] lineParts)
        {
            return decimal.Parse(lineParts[2], CultureInfo.InvariantCulture);
        }

        private static string GetDestinationCurrency(string[] lineParts)
        {
            return lineParts[1];
        }

        private static Money BuildInitialMoney(string[] firstLineParts)
        {
            return new Money(
                BuildInitialCurrency(firstLineParts),
                BuildInitialAmount(firstLineParts));
        }

        private static string BuildInitialCurrency(string[] lineParts)
        {
            return lineParts[0];
        }

        private static decimal BuildInitialAmount(string[] lineParts)
        {
            return decimal.Parse(lineParts[1]);
        }

        private static string BuildTargetCurrency(string[] lineParts)
        {
            return lineParts[2];
        }
    }
}
