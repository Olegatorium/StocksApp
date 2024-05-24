using Microsoft.Extensions.Options;
using StocksApp.Models;

namespace StocksApp.ServiceContracts
{
    public interface IFinnhubService
    {
         Task <Dictionary<string, object>?> GetStockInformation(string stockSymbol, string information);

         Task<List<Dictionary<string, object>>> GetCompanyNewsInformation(string stockSymbol);

         Task<List<Stock>> GetSymbolsInfo(IOptions<TradingOptions> tradingOptions);

         Task<List<Dictionary<string, string>>?> GetStocks();

         Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);

         Task<List<StockShortInfo>> ConvertToStockList(List<Dictionary<string, string>>? stocks);
    }
}
