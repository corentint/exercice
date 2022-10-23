using Lucca_Suite;

internal class Program
{
    private static void Main(string[] args)
    {
        //string pathRead = args[0];
        string pathRead = "C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\input.txt";

        var currencyConverter =  new CurrencyConverter(new StringToObjectsAdapter(new FileReader(pathRead)));
        
        Console.WriteLine(currencyConverter.GetResult());

    }
}