using Lucca_Suite;

namespace Luccas_Suite_Test
{
    public class FileReaderTest
    {
        const string VALID_FILE_NAME = "input.txt";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitialMoney_Init_WrongFileName_Throws()
        {
            // Act
            Assert.Throws<FileNotFoundException>(() => new FileReader("non_exting_filename.txt"));
        }

        [Test]
        public void InitialMoney_Init_NotExistingDirectory_Throws()
        {
            // Act
            Assert.Throws<DirectoryNotFoundException>(() => new FileReader("C:\\NonExistingDirectory\\input.txt"));
        }

        [Test]
        public void InitialMoney_Init_ValidFile_DoesNotThrow()
        {
            // Arrange
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), VALID_FILE_NAME);

            // Act
            Assert.DoesNotThrow(() => new FileReader(filePath));
        }
    }
}