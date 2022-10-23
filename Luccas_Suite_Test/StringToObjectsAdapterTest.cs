using Lucca_Suite;

namespace Luccas_Suite_Test
{
    public class StringToObjectsAdapterTest
    {
        const string VALID_FILE_NAME = "input.txt";
        private FileReader _fileReader;

        [SetUp]
        public void Setup()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), VALID_FILE_NAME);
            _fileReader = new FileReader(filePath);
        }

        [Test]
        public void InitialMoney_Init_ValidFile_ExchangeRatesAreImported()
        {
            // Act
            var stringToObjectsAdapter = new StringToObjectsAdapter(_fileReader);

            // Assert
            Assert.That(stringToObjectsAdapter.ExchangeRates.Count, Is.EqualTo(12));
            Assert.That(stringToObjectsAdapter.ExchangeRates[0].SourceCurrencySymbol, Is.EqualTo("AUD"));
            Assert.That(stringToObjectsAdapter.ExchangeRates[0].DestinationCurrencySymbol, Is.EqualTo("CHF"));
            Assert.That(stringToObjectsAdapter.ExchangeRates[0].Rate, Is.EqualTo(0.9661));
        }

        [Test]
        public void InitialMoney_Init_ValidFile_FirstLineIsImported()
        {
            // Act
            var stringToObjectsAdapter = new StringToObjectsAdapter(_fileReader);

            // Assert
            Assert.That(stringToObjectsAdapter.InitialMoney.Amount, Is.EqualTo(550));
            Assert.That(stringToObjectsAdapter.InitialMoney.Currency, Is.EqualTo("EUR"));
            Assert.That(stringToObjectsAdapter.TargetCurrency, Is.EqualTo("JPY"));
        }
    }
}