using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.ServiceContracts;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IStocksService _stocksService;

        public StocksController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IStocksService stocksService)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
            _stocksService = stocksService;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Explore()
        {
            List<Dictionary<string, string>>? stocks = await _finnhubService.GetStocks();

            var stocksShortInfo = await _finnhubService.ConvertToStockList(stocks);

            return View(stocksShortInfo);
        }
    }
}
