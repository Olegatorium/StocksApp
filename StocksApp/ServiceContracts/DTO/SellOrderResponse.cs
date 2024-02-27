using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string StockSymbol { get; set; }
        public string StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID, StockSymbol = sellOrder.StockSymbol, StockName = sellOrder.StockName, 
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder, Quantity = sellOrder.Quantity, Price = sellOrder.Price
            };
        }
    }
}
