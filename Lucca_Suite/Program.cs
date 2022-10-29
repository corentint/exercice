using Lucca_Suite;
using Lucca_Suite.Algorithm;
using Lucca_Suite.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        string path = args[0];

        var lines = FileReader.ReadAllLines(path);

        var currencyData = CurrencySerializer.Deserialize(lines);

        IShortestPathFinder algorithm = new BFSAlgorithm();

        var currencyConverter =  new CurrencyConverter(algorithm);
        
        Console.WriteLine(currencyConverter.GetResult(currencyData));

    }
}