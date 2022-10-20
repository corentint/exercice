using Lucca_Suite;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        // Console.WriteLine("Enter file path : ");
        //string pathRead = Console.ReadLine();
        string pathRead = "C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\input.txt";

        // C:\Users\Corentin\source\repos\Lucca_Suite\Lucca_Suite\input.txt
        string text = File.ReadAllText(pathRead);

        //var currencies = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
        //    .Select(ci => ci.LCID).Distinct()
        //    .Select(id => new RegionInfo(id))
        //    .Select(r => r.ISOCurrencySymbol).Distinct();

        //foreach (var currency in currencies)
        //{
        //    Console.WriteLine($"currency : {currency}");
        //}
        //// 107 currencies
        //Console.WriteLine($"number of currency : {currencies.Count()}");

        // Store all ExchangeRates
        var lines = File.ReadAllLines(pathRead);
        List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
        var firstLineParts = lines[0].Split(";");
        var initialCurrency = firstLineParts[0];
        var startingAmount = firstLineParts[1];
        var targetCurrency = firstLineParts[2];

        var numberOfExchangeRates = lines[1];

        

        Console.WriteLine(text);

        foreach (var exchangeRate in exchangeRates)
        {
            Console.WriteLine($"we can convert : {exchangeRate.SourceCurrencySymbol} to {exchangeRate.DestinationCurrencySymbol} with a rate of : {exchangeRate.Rate}");
        }

        Console.WriteLine($"initial amount : {startingAmount} in {initialCurrency} into {targetCurrency}");
    }
}