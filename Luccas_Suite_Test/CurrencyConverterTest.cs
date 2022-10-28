using Lucca_Suite;
using Lucca_Suite.Model;

namespace Luccas_Suite_Test
{
    public class CurrencyConverterTest
    {
        [Test]
        public void CurrencyConverter_GetResult_ReturnsExpectedResult()
        {
            // Arrange
            var currencyData = new CurrencyData()
            {
                InitialMoney = new Money("EUR", 550),
                TargetCurrency = "JPY",
                ExchangeRates = new List<ExchangeRate>
                {
                    new ExchangeRate("AUD", "CHF", 0.9661m),
                    new ExchangeRate("CHF", "AUD", 1/0.9661m),
                    new ExchangeRate("JPY", "KRW", 13.1151m),
                    new ExchangeRate("KRW", "JPY", 1/13.1151m),
                    new ExchangeRate("EUR", "CHF", 1.2053m),
                    new ExchangeRate("CHF", "EUR", 1/1.2053m),
                    new ExchangeRate("AUD", "JPY", 86.0305m),
                    new ExchangeRate("JPY", "AUD", 1/86.0305m),
                    new ExchangeRate("EUR", "USD", 1.2989m),
                    new ExchangeRate("USD", "EUR", 1/1.2989m),
                    new ExchangeRate("JPY", "INR", 0.6571m),
                    new ExchangeRate("INR", "JPY", 1/0.6571m),
                }
            };
            var currencyConverter = new CurrencyConverter(currencyData);

            // Act
            var result = currencyConverter.GetResult();

            // Assert
            Assert.That(result, Is.EqualTo(59033));
        }

        [Test]
        public void CurrencyConverter_GetResult_NoPossiblePath_ThrowsException()
        {
            // Arrange
            var currencyData = new CurrencyData()
            {
                InitialMoney = new Money("EUR", 550),
                TargetCurrency = "JPY",
                ExchangeRates = new List<ExchangeRate>
                {
                    new ExchangeRate("AUD", "CHF", 0.9661m),
                    new ExchangeRate("CHF", "AUD", 1/0.9661m),
                    new ExchangeRate("JPY", "KRW", 13.1151m),
                    new ExchangeRate("KRW", "JPY", 1/13.1151m),
                    new ExchangeRate("EUR", "CHF", 1.2053m),
                    new ExchangeRate("CHF", "EUR", 1/1.2053m),
                    new ExchangeRate("EUR", "USD", 1.2989m),
                    new ExchangeRate("USD", "EUR", 1/1.2989m),
                    new ExchangeRate("JPY", "INR", 0.6571m),
                    new ExchangeRate("INR", "JPY", 1/0.6571m),
                }
            };
            var currencyConverter = new CurrencyConverter(currencyData);

            // Act - Assert
            Assert.Throws<KeyNotFoundException>(() => currencyConverter.GetResult());
        }

        [Test]
        public void CurrencyConverter_GetResult_ValidInput_ReturnsExpectedResult()
        {
            // Arrange
            var currencyData = new CurrencyData()
            {
                InitialMoney = new Money("EUR", 550),
                TargetCurrency = "JPY",
                ExchangeRates = new List<ExchangeRate>
                {
                    new ExchangeRate("AUD", "CHF", 0.9661m),
                    new ExchangeRate("CHF", "AUD", 1/0.9661m),
                    new ExchangeRate("JPY", "KRW", 13.1151m),
                    new ExchangeRate("KRW", "JPY", 1/13.1151m),
                    new ExchangeRate("EUR", "CHF", 1.2053m),
                    new ExchangeRate("CHF", "EUR", 1/1.2053m),
                    new ExchangeRate("AUD", "JPY", 86.0305m),
                    new ExchangeRate("JPY", "AUD", 1/86.0305m),
                    new ExchangeRate("EUR", "USD", 1.2989m),
                    new ExchangeRate("USD", "EUR", 1/1.2989m),
                    new ExchangeRate("JPY", "INR", 0.6571m),
                    new ExchangeRate("INR", "JPY", 1/0.6571m),
                }
            };
            var currencyConverter = new CurrencyConverter(currencyData);

            // Act
            var result = currencyConverter.GetResult();

            // Assert
            Assert.That(result, Is.EqualTo(59033));
        }
    }
}