using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using StocksApp.Services;
using System.Globalization;

namespace StocksApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IStocksService _stocksService;


        public HomeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IStocksService stocksService)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
            _stocksService = stocksService;
        }


        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }

            Dictionary<string, object>? responseStockPriceQuote = await
                _finnhubService.GetStockInformation(_tradingOptions.Value.DefaultStockSymbol, "StockPriceQuote");

            Dictionary<string, object>? responseCompanyName = await
                _finnhubService.GetStockInformation(_tradingOptions.Value.DefaultStockSymbol, "CompanyProfile");


            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
                Price = Convert.ToDecimal(responseStockPriceQuote["c"].ToString(), CultureInfo.InvariantCulture),
                StockName = responseCompanyName["name"].ToString(),
            };


            return View(stock);
        }

        [Route("CompanyNews")]
        public async Task<IActionResult> СompanyNews()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }


           var responseСompanyNews = await
                _finnhubService.GetCompanyNewsInformation(_tradingOptions.Value.DefaultStockSymbol);

            if (responseСompanyNews == null)
            {
                return BadRequest("Failed to retrieve company news.");
            }


            List<CompanyNews> companyNews = new List<CompanyNews>();

            foreach (var item in responseСompanyNews)
            {
                companyNews.Add(new CompanyNews()
                {
                    Category = item["category"].ToString(),
                    HeadLine = item["headline"].ToString(),
                    Related = item["related"].ToString(),
                    Source = item["source"].ToString(),
                    Summary = item["summary"].ToString()
                });
            }

            return View(companyNews);
            
        }
    }
}