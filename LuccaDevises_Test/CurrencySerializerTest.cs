using LuccaDevises;

namespace Luccas_Suite_Test
{
    public class CurrencySerializerTest
    {
        private string[] _lines;

        [SetUp]
        public void Setup()
        {
            _lines = new string[]
            {
                "EUR;550;JPY",
                "6",
                "AUD;CHF;0.9661",
                "JPY;KRW;13.1151",
                "EUR;CHF;1.2053",
                "AUD;JPY;86.0305",
                "EUR;USD;1.2989",
                "JPY;INR;0.6571"
            };
        }

        [Test]
        public void CurrencySerializer_Deserialize_ValidFile_ExchangeRatesAreImported()
        {
            // Act
            var currencyData = CurrencySerializer.Deserialize(_lines);

            // Assert
            Assert.That(currencyData.ExchangeRates.Count, Is.EqualTo(12));
            Assert.That(currencyData.ExchangeRates[0].SourceCurrencySymbol, Is.EqualTo("AUD"));
            Assert.That(currencyData.ExchangeRates[0].DestinationCurrencySymbol, Is.EqualTo("CHF"));
            Assert.That(currencyData.ExchangeRates[0].Rate, Is.EqualTo(0.9661));
        }

        [Test]
        public void CurrencySerializer_Deserialize_ValidFile_FirstLineIsImported()
        {
            // Act
            var currencyData = CurrencySerializer.Deserialize(_lines);

            // Assert
            Assert.That(currencyData.InitialMoney.Amount, Is.EqualTo(550));
            Assert.That(currencyData.InitialMoney.Currency, Is.EqualTo("EUR"));
            Assert.That(currencyData.TargetCurrency, Is.EqualTo("JPY"));
        }
    }
}