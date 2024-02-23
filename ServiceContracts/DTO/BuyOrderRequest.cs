using System.ComponentModel.DataAnnotations;

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
    }
}
