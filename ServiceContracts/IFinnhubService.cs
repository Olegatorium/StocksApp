namespace StocksApp.ServiceContracts
{
    public interface IFinnhubService
    {
         Task <Dictionary<string, object>?> GetStockInformation(string stockSymbol, string information);

         Task<List<Dictionary<string, object>>> GetCompanyNewsInformation(string stockSymbol);
    }
}
