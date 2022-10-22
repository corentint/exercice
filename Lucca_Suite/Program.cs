using Lucca_Suite;

internal class Program
{
    private static void Main(string[] args)
    {
        //string pathRead = args[0];
        string pathRead = "C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\inputModified.txt";

        var currencyConverter =  new CurrencyConverter(new FileReader(pathRead));
        
        Console.WriteLine(currencyConverter.GetResult());

    }
}