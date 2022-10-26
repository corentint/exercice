using Lucca_Suite;

internal class Program
{
    private static void Main(string[] args)
    {
        string path = args[0];

        var lines = FileReader.ReadAllLines(path);

        var currencyData = CurrencySerializer.Deserialize(lines);

        var currencyConverter =  new CurrencyConverter(currencyData);
        
        Console.WriteLine(currencyConverter.GetResult());

    }
}