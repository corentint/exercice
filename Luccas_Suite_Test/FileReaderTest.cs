using Lucca_Suite;

namespace Luccas_Suite_Test
{
    public class FileReaderTest
    {
        const string VALID_FILE_PATH = "Files\\valid.txt";
        const string NON_EXISTING_FILE_NAME = "non_exting_filename.txt";
        const string NON_EXISTING_DIRECTORY_NAME = "C:\\NonExistingDirectory\\input.txt";

        [Test]
        public void FileReader_WrongFileName_Throws()
        {
            // Act
            Assert.Throws<FileNotFoundException>(() => new FileReader(NON_EXISTING_FILE_NAME));
        }

        [Test]
        public void FileReader_NotExistingDirectory_Throws()
        {
            // Act
            Assert.Throws<DirectoryNotFoundException>(() => new FileReader(NON_EXISTING_DIRECTORY_NAME));
        }

        [Test]
        public void FileReader_ValidFile_DoesNotThrow()
        {
            // Arrange
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), VALID_FILE_PATH);

            // Act
            Assert.DoesNotThrow(() => new FileReader(filePath));
        }

        [Test]
        public void FileReader_NoPossiblePath_DoesNotThrow()
        {
            // Arrange
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), VALID_FILE_PATH);

            // Act
            Assert.DoesNotThrow(() => new FileReader(filePath));
        }
    }
}