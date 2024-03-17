using StocksApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
    public  class FinnhubServiceTest
    {
        private readonly IFinnhubService _finnhubService;

        public FinnhubServiceTest(IFinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        #region GetSymbolsInfo

        [Fact]

        public async Task GetSymbolsInfo_ProperDetails() 
        {
            //var list = await _finnhubService.GetSymbolsInfo();
        }

        #endregion
    }
}
