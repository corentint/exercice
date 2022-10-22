using Lucca_Suite;

namespace Luccas_Suite_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitialMoney_Init_WrongFileName_Throws()
        {
            Assert.Throws<FileNotFoundException>(() => new FileReader("non_exting_filename.txt"));
        }

        [Test]
        public void InitialMoney_Init_NotExistingDirectory_Throws()
        {
            Assert.Throws<DirectoryNotFoundException>(() => new FileReader("C:\\Users\\NonExistingUser\\input.txt"));
        }

        [Test]
        public void InitialMoney_Init_ValidFile_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new FileReader("C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\input.txt"));
        }

        [Test]
        public void InitialMoney_Init_ValidFile_ExchangeRatesAreImported()
        {
            var fileReader = new FileReader("C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\input.txt");

            Assert.That(fileReader.ExchangeRates.Count, Is.EqualTo(6));
            Assert.That(fileReader.ExchangeRates[0].SourceCurrencySymbol, Is.EqualTo("AUD"));
            Assert.That(fileReader.ExchangeRates[0].DestinationCurrencySymbol, Is.EqualTo("CHF"));
            Assert.That(fileReader.ExchangeRates[0].Rate, Is.EqualTo(0.9661));
        }

        [Test]
        public void InitialMoney_Init_ValidFile_FirstLineIsImported()
        {
            var fileReader = new FileReader("C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\input.txt");

            Assert.That(fileReader.InitialMoney.Amount, Is.EqualTo(550));
            Assert.That(fileReader.InitialMoney.Currency, Is.EqualTo("EUR"));
            Assert.That(fileReader.TargetCurrency, Is.EqualTo("JPY"));
        }
    }
}