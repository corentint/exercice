namespace Lucca_Suite.Model
{
    public class CurrencyData
    {
        public List<ExchangeRate> ExchangeRates { get; set; }
        public Money InitialMoney { get; set; }
        public string TargetCurrency { get; set; }

        public CurrencyData(List<ExchangeRate> exchangeRates, Money initialMoney, string targetCurrency)
        {
            ExchangeRates = exchangeRates;
            InitialMoney = initialMoney;
            TargetCurrency = targetCurrency;
        }
    }
}
