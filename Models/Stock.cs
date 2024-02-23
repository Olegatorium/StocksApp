namespace StocksApp.Models
{
    public class Stock
    {
        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public decimal Price { get; set; }

        public uint Quantity { get; set; }

        public decimal tenDayAverageTradingVolume { get; set;}
    }
}
