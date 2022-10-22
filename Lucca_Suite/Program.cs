using Lucca_Suite;

internal class Program
{
    private static void Main(string[] args)
    {
        // Console.WriteLine("Enter file path : ");
        //string pathRead = Console.ReadLine();
        string pathRead = "C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\inputModified.txt";

        var currencyConverter =  new CurrencyConverter(new FileReader(pathRead));
        
        Console.WriteLine($"The result is : {currencyConverter.GetResult()}");

    }
}