namespace Lucca_Suite
{
    public class FileReader
    {
        public static string[] ReadAllLines(string filePath)
        {
            try
            {
                return File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while reading the file path {filePath} : {e.Message}");
                
                // Could be logging or not rethrowing
                throw;
            }
        }
    }
}