using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        /// <summary>
        /// Compares the current object data with the parameter object
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(BuyOrderResponse)) return false;

            BuyOrderResponse buyOrderResponse = (BuyOrderResponse)obj;

            return BuyOrderID == buyOrderResponse.BuyOrderID && StockSymbol == buyOrderResponse.StockSymbol && StockName == buyOrderResponse.StockName &&
                DateAndTimeOfOrder == buyOrderResponse.DateAndTimeOfOrder && Quantity == buyOrderResponse.Quantity && Price == buyOrderResponse.Price  &&
                  TradeAmount == buyOrderResponse.TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Buy Order ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date and Time of Buy Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, Quantity: {Quantity}, Buy Price: {Price}, Trade Amount: {TradeAmount}";
        }
    }

    public static class BuyOrderExtension
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse() { BuyOrderID = buyOrder.BuyOrderID, StockSymbol = buyOrder.StockSymbol, StockName = buyOrder.StockName, 
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder, Price = buyOrder.Price, Quantity = buyOrder.Quantity,
                TradeAmount = buyOrder.Price * buyOrder.Quantity
            };
        }
    }
}
