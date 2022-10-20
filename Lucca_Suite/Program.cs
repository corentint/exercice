using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        // Console.WriteLine("Enter file path : ");
        //string pathRead = Console.ReadLine();

        // C:\Users\Corentin\source\repos\Lucca_Suite\Lucca_Suite\input.txt
        //string text = File.ReadAllText(pathRead);
        string text = File.ReadAllText("C:\\Users\\Corentin\\source\\repos\\Lucca_Suite\\Lucca_Suite\\input.txt");

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


        //Console.WriteLine(text);

        //Console.WriteLine("end execution");
    }
}