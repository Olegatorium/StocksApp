using StocksApp.ServiceContracts;
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
        
    

