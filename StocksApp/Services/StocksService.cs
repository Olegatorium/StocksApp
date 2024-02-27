using Entity;
using ServiceContracts.DTO;
using Services.Helpers;
using StocksApp.ServiceContracts;

namespace StocksApp.Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;

        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
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

            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _sellOrders.Add(sellOrder);

            return Task.FromResult(sellOrder.ToSellOrderResponse());
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            return Task.FromResult(_sellOrders.Select(x => x.ToSellOrderResponse()).ToList());
        }
    }
}
