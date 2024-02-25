using Entity;
using ServiceContracts.DTO;
using Services.Helpers;
using StocksApp.ServiceContracts;

namespace StocksApp.Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
        }

        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _buyOrders.Add(buyOrder);

            return Task.FromResult(buyOrder.ToBuyOrderResponse());
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return Task.FromResult(_buyOrders.Select(x => x.ToBuyOrderResponse()).ToList());
        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            throw new NotImplementedException();
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            throw new NotImplementedException();
        }
    }
}
