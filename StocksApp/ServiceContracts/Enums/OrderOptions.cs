namespace StocksApp.ServiceContracts.Enums
{
    public enum OrderOptions
    {
        BuyOrder, SellOrder
    }
    public static class OrderOptionsExtensions
    {
        public static string ToFormattedOrderOptionString(this OrderOptions option)
        {
            switch (option)
            {
                case OrderOptions.BuyOrder:
                    return "Buy Order";
                case OrderOptions.SellOrder:
                    return "Sell Order";
                default:
                    throw new InvalidOperationException("Unexpected OrderOptions value.");
            }
        }
    }
}
