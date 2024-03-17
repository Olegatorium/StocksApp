﻿using ServiceContracts.DTO;
using StocksApp.ServiceContracts.DTO;

namespace StocksApp.ServiceContracts
{
    public interface IStocksService
    {
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        Task<List<BuyOrderResponse>> GetBuyOrders();

        Task<List<SellOrderResponse>> GetSellOrders();
        Task<List<OrdersResponse>> GetAllOrders();

    }
}
