using Lucca_Suite;

internal class Program
{
    private static void Main(string[] args)
    {
        //string pathRead = args[0];
        string pathRead = "C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\input.txt";

        var fileReader = new FileReader(pathRead);

        var currencyData = CurrencySerializer.Deserialize(fileReader.Lines);

        var currencyConverter =  new CurrencyConverter(currencyData);
        
        Console.WriteLine(currencyConverter.GetResult());

    }
}