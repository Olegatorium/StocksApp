using Entity;
using ServiceContracts.DTO;
using StocksApp.ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace StocksApp.ServiceContracts.DTO
{
    public class OrdersResponse
    { 
        public Guid BuyOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public string? TypeOfOrder { get; set; }

        public double TradeAmount { get; set; }

    }

    public static class OrdersExtension
    {
        public static OrdersResponse ToOrderResponseFromBuyOrder(this BuyOrder buyOrder)
        {
            return new OrdersResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Price = buyOrder.Price,
                Quantity = buyOrder.Quantity,
                TradeAmount = buyOrder.Price * buyOrder.Quantity,
                TypeOfOrder = OrderOptions.BuyOrder.ToFormattedOrderOptionString()
            };
        }

        public static OrdersResponse ToOrderResponseFromSellOrder(this SellOrder sellOrder)
        {
            return new OrdersResponse()
            {
                BuyOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Price = sellOrder.Price,
                Quantity = sellOrder.Quantity,
                TradeAmount = sellOrder.Price * sellOrder.Quantity,
                TypeOfOrder = OrderOptions.SellOrder.ToFormattedOrderOptionString()
            };
        }
    }
}
