using Entity;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.DTO;
using Services.Helpers;
using StocksApp.ServiceContracts;

namespace StocksApp.Services
{
    public class StocksService : IStocksService
    {
        private StockMarketDbContext _db;

        public StocksService(StockMarketDbContext stockMarketDbContext)
        {
            _db = stockMarketDbContext;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return await _db.BuyOrders.Select(x => x.ToBuyOrderResponse()).ToListAsync();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return await _db.SellOrders.Select(x => x.ToSellOrderResponse()).ToListAsync();
        }
    }
}
