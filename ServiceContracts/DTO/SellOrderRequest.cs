using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderRequest
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

        public SellOrder ToSellOrder()
        {
            return new SellOrder() {StockSymbol = StockSymbol, StockName = StockName, DateAndTimeOfOrder = DateAndTimeOfOrder, Quantity = Quantity, Price = Price };
        }

    }
}
