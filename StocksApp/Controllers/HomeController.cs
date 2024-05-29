using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts.DTO;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using StocksApp.ServiceContracts.DTO;
using StocksApp.Services;
using System;
using System.Diagnostics;
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

        [Route("[action]/{stock?}")]
        public async Task<IActionResult> TradeNow(string? stock) 
        {
            if (stock == null)
            {
                stock = "MSFT";
            }

            var foundStockCompanyName = await _finnhubService.GetStockInformation(stock, "CompanyProfile");

            var foundStockPriceQuote = await _finnhubService.GetStockInformation(stock, "StockPriceQuote");

            Stock stockInfo = new Stock()
            {
                StockSymbol = foundStockCompanyName["ticker"].ToString(),
                Price = Convert.ToDecimal(foundStockPriceQuote["c"].ToString(), CultureInfo.InvariantCulture),
                StockName = foundStockCompanyName["name"].ToString(),
            };

            return View(stockInfo);
        }

        [Route("[action]")]
        public async Task<IActionResult> Orders() 
        {
            //invoke service methods
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            //create model object
            Orders orders = new Orders() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };

            ViewBag.TradingOptions = _tradingOptions;

            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            //update date of order
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            //re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);


            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Stock stockTrade = new Stock() { StockName = buyOrderRequest.StockName, Quantity = buyOrderRequest.Quantity, StockSymbol = buyOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            //invoke service method
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction("Orders", "Home");
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            //update date of order
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            //re-validate the model object after updating the date
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Stock stockTrade = new Stock() { StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol };
                return View("Index", stockTrade);
            }

            //invoke service method
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            return RedirectToAction("Orders", "Home");
        }
       
        [Route("/")]
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }

            List<Stock> stockList = await _finnhubService.GetSymbolsInfo(_tradingOptions);

            return View(stockList);
        }

        [Route("[action]")]
        public async Task<IActionResult> CompanyNews([FromQuery] string stockSymbol)
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }


           var responseСompanyNews = await
                _finnhubService.GetCompanyNewsInformation(stockSymbol);

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

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            List<OrdersResponse> listOrders = await _stocksService.GetAllOrders();

            return new ViewAsPdf("OrdersPDF", listOrders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}