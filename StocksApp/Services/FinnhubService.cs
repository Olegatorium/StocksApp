using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using System.Globalization;
using System.Text.Json;

namespace StocksApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<Stock>> GetSymbolsInfo(IOptions<TradingOptions> tradingOptions)
        {
            Dictionary<string, object>? responseStockPriceQuoteMSFT = await
                  GetStockInformation(tradingOptions.Value.DefaultStockSymbol, "StockPriceQuote");

            Dictionary<string, object>? responseCompanyNameMSFT = await
                  GetStockInformation(tradingOptions.Value.DefaultStockSymbol, "CompanyProfile");

            Dictionary<string, object>? responseStockPriceQuoteAAPL = await
                  GetStockInformation("AAPL", "StockPriceQuote");

            Dictionary<string, object>? responseCompanyNameAAPL = await
                  GetStockInformation("AAPL", "CompanyProfile");

            Dictionary<string, object>? responseStockPriceQuoteAMZN = await
                GetStockInformation("AMZN", "StockPriceQuote");

            Dictionary<string, object>? responseCompanyNameAMZN = await
                GetStockInformation("AMZN", "CompanyProfile");

            Dictionary<string, object>? responseStockPriceQuoteGOOGL = await
                GetStockInformation("GOOGL", "StockPriceQuote");

            Dictionary<string, object>? responseCompanyNameGOOGL = await
                GetStockInformation("GOOGL", "CompanyProfile");

            Dictionary<string, object>? responseStockPriceQuoteTSLA = await
                GetStockInformation("TSLA", "StockPriceQuote");

            Dictionary<string, object>? responseCompanyNameTSLA = await
                GetStockInformation("TSLA", "CompanyProfile");


            Stock stockMSFT = new Stock()
            {
                StockSymbol = tradingOptions.Value.DefaultStockSymbol,
                Price = Convert.ToDecimal(responseStockPriceQuoteMSFT["c"].ToString(), CultureInfo.InvariantCulture),
                StockName = responseCompanyNameMSFT["name"].ToString(),
            };

            Stock stockAAPL = new Stock()
            {
                StockSymbol = responseCompanyNameAAPL["ticker"].ToString(),
                Price = Convert.ToDecimal(responseStockPriceQuoteAAPL["c"].ToString(), CultureInfo.InvariantCulture),
                StockName = responseCompanyNameAAPL["name"].ToString(),
            };

            Stock stockAMZN = new Stock()
            {
                StockSymbol = responseCompanyNameAMZN["ticker"].ToString(),
                Price = Convert.ToDecimal(responseStockPriceQuoteAMZN["c"].ToString(), CultureInfo.InvariantCulture),
                StockName = responseCompanyNameAMZN["name"].ToString(),
            };

            Stock stockGOOGL = new Stock()
            {
                StockSymbol = responseCompanyNameGOOGL["ticker"].ToString(),
                Price = Convert.ToDecimal(responseStockPriceQuoteGOOGL["c"].ToString(), CultureInfo.InvariantCulture),
                StockName = responseCompanyNameGOOGL["name"].ToString(),
            };

            Stock stockTSLA = new Stock()
            {
                StockSymbol = responseCompanyNameTSLA["ticker"].ToString(),
                Price = Convert.ToDecimal(responseStockPriceQuoteTSLA["c"].ToString(), CultureInfo.InvariantCulture),
                StockName = responseCompanyNameTSLA["name"].ToString(),
            };

            List<Stock> stockList = new List<Stock>() { stockMSFT, stockAAPL, stockAMZN, stockGOOGL, stockTSLA };

            return stockList;
        }

        public async Task<Dictionary<string, object>?> GetStockInformation(string stockSymbol, string requstedInfo)
        {
            string uri = "";

            if (requstedInfo == "StockPriceQuote")
            {
                uri = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}";
            }
            else if (requstedInfo == "CompanyProfile")
            {
                uri = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}";
            }

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{uri}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);


                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (responseDictionary == null)
                    throw new InvalidOperationException("No response from finnhub server");

                if (responseDictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

                return responseDictionary;
            }
        }

        public async Task<List<Dictionary<string, object>>> GetCompanyNewsInformation(string stockSymbol)
        {

            string uri = $"https://finnhub.io/api/v1/company-news?symbol={stockSymbol}&from=2023-08-15&to=2023-08-20";

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{uri}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                List<Dictionary<string, object>>? companyNewsList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(response);

                if (companyNewsList == null)
                    throw new InvalidOperationException("No response from finnhub server");

                return companyNewsList;
            }
        }
    }
}
        
    

