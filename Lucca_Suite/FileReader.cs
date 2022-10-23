namespace Lucca_Suite
{
    public class FileReader
    {
        public string[] Lines { get; }

        public FileReader(string filePath)
        {
            Lines = File.ReadAllLines(filePath);
        }
    }
}