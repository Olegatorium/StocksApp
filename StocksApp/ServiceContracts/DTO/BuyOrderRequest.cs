using Entity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ServiceContracts.DTO
{
    public class BuyOrderRequest
    {
        [Required]
        public string StockSymbol { get; set; }

        [Required]
        public string StockName { get; set; }

        [DateShouldBeAfter("2000-01-01", ErrorMessage = "Date should not be older than Jan 01, 2000")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1, 10000)]
        public double Price { get; set; }

        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
