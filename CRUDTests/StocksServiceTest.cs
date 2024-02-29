using ServiceContracts.DTO;
using StocksApp.ServiceContracts;
using StocksApp.Services;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;
        private readonly ITestOutputHelper _testOutputHelper;

        public StocksServiceTest(ITestOutputHelper testOutputHelper)
        {
            _stocksService = new StocksService();
            _testOutputHelper = testOutputHelper;
        }

        #region CreateBuyOrder

        //When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task AddOrder_NullBuyOrder()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task AddOrder_LessOrderQuantity()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                BuyOrderRequest buyOrderRequest = new BuyOrderRequest
                {
                    Quantity = 0,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 100,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });

        }

        //When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public async Task AddOrder_GreaterOrderQuantity()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                BuyOrderRequest buyOrderRequest = new BuyOrderRequest
                {
                    Quantity = 100001,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 100,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }


        //When you supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task AddOrder_LessOrderPrice()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                BuyOrderRequest buyOrderRequest = new BuyOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 0,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public async Task AddOrder_GreaterOrderPrice()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                BuyOrderRequest buyOrderRequest = new BuyOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 10001,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task AddOrder_NullStockSymbol()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                BuyOrderRequest buyOrderRequest = new BuyOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 555,
                    StockName = "MSFT",
                    StockSymbol = null
                };

                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When you supply incorrect dateAndTimeOfOrder
        [Fact]
        public async Task AddOrder_InvalidDateAndTimeOfOrder()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                BuyOrderRequest buyOrderRequest = new BuyOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("1999-02-02"),
                    Price = 555,
                    StockName = "MSFT",
                    StockSymbol = "GSP"
                };

                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //If you supply all valid values, it should be successful and return an object of BuyOrderResponse
        [Fact]
        public async Task AddOrder_ValidValues()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest
            {
                Quantity = 333,
                DateAndTimeOfOrder = DateTime.Parse("2002-02-02"),
                Price = 555,
                StockName = "MSFT",
                StockSymbol = "GSP"
            };

            //Act
            var buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            List<BuyOrderResponse> buyOrders_list = await _stocksService.GetBuyOrders();

            //Assert

            Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);

            Assert.Contains(buyOrderResponse, buyOrders_list);
        }
        #endregion

        #region GetBuyOrders

        //When you invoke this method, by default, the returned list should be empty
        [Fact]
        public async Task GetBuyOrders_DefaultList()
        {
            List<BuyOrderResponse> buyOrders_list = await _stocksService.GetBuyOrders();

            Assert.Empty(buyOrders_list);
        }

        [Fact]
        public async Task GetBuyOrders_ProperDetails()
        {
            List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest>()
            {

            new BuyOrderRequest
            {
                Quantity = 444,
                DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                Price = 444,
                StockName = "MSFT",
                StockSymbol = "GST"
            },

            new BuyOrderRequest
            {
                Quantity = 222,
                DateAndTimeOfOrder = DateTime.Parse("2006-02-02"),
                Price = 234,
                StockName = "MSas",
                StockSymbol = "Gdd"
            },

            new BuyOrderRequest
            {
                Quantity = 2,
                DateAndTimeOfOrder = DateTime.Parse("2012-02-02"),
                Price = 1000,
                StockName = "MeeT",
                StockSymbol = "GfT"
            }
        };

            List<BuyOrderResponse> expectedBuyOrderList = new List<BuyOrderResponse>();

            foreach (var item in buyOrderRequests)
            {
                expectedBuyOrderList.Add(await _stocksService.CreateBuyOrder(item));
            }

            List<BuyOrderResponse> actualBuyOrdersList = await _stocksService.GetBuyOrders();

            foreach (var item in expectedBuyOrderList)
            {
                Assert.Contains(item, actualBuyOrdersList);
            }
        }

        #endregion

        #region CreateSellOrder

        //When you supply SellOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task CreateSell_NullSellOrder()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateSell_LessOrderQuantity()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                SellOrderRequest sellOrderRequest = new SellOrderRequest
                {
                    Quantity = 0,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 100,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateSellOrder(sellOrderRequest);
            });

        }

        //When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public async Task CreateSell_GreaterOrderQuantity()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                SellOrderRequest sellOrderRequest = new SellOrderRequest
                {
                    Quantity = 100001,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 100,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When you supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateSell_LessOrderPrice()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                SellOrderRequest sellOrderRequest = new SellOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 0,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public async Task CreateSell_GreaterOrderPrice()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                SellOrderRequest sellOrderRequest = new SellOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 10001,
                    StockName = "MSFT",
                    StockSymbol = "GST"
                };

                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When you supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateSell_NullStockSymbol()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                SellOrderRequest sellOrderRequest = new SellOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                    Price = 555,
                    StockName = "MSFT",
                    StockSymbol = null
                };

                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When you supply incorrect dateAndTimeOfOrder
        [Fact]
        public async Task CreateSell_InvalidDateAndTimeOfOrder()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                SellOrderRequest sellOrderRequest = new SellOrderRequest
                {
                    Quantity = 333,
                    DateAndTimeOfOrder = DateTime.Parse("1999-02-02"),
                    Price = 555,
                    StockName = "MSFT",
                    StockSymbol = "GSP"
                };

                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //If you supply all valid values, it should be successful and return an object of SellOrderResponse
        [Fact]
        public async Task CreateSell_ValidValues()
        {
            //Arrange
            SellOrderRequest buyOrderRequest = new SellOrderRequest
            {
                Quantity = 333,
                DateAndTimeOfOrder = DateTime.Parse("2002-02-02"),
                Price = 555,
                StockName = "MSFT",
                StockSymbol = "GSP"
            };

            //Act
            var sellOrderResponse = await _stocksService.CreateSellOrder(buyOrderRequest);

            List<SellOrderResponse> buyOrders_list = await _stocksService.GetSellOrders();

            //Assert

            Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);

            Assert.Contains(sellOrderResponse, buyOrders_list);
        }
        #endregion

        #region GetSellOrders

        //When you invoke this method, by default, the returned list should be empty
        [Fact]
        public async Task GetSellOrders_DefaultList()
        {
            List<SellOrderResponse> sellOrders_list = await _stocksService.GetSellOrders();

            Assert.Empty(sellOrders_list);
        }

        [Fact]
        public async Task  GetSellOrders_ProperDetails()
        {
            List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>()
            {

            new SellOrderRequest
            {
                Quantity = 444,
                DateAndTimeOfOrder = DateTime.Parse("2004-02-02"),
                Price = 444,
                StockName = "MSFT",
                StockSymbol = "GST"
            },

            new SellOrderRequest
            {
                Quantity = 222,
                DateAndTimeOfOrder = DateTime.Parse("2006-02-02"),
                Price = 234,
                StockName = "MSas",
                StockSymbol = "Gdd"
            },

            new SellOrderRequest
            {
                Quantity = 2,
                DateAndTimeOfOrder = DateTime.Parse("2012-02-02"),
                Price = 1000,
                StockName = "MeeT",
                StockSymbol = "GfT"
            }
        };

            List<SellOrderResponse> expectedSellOrderList = new List<SellOrderResponse>();

            foreach (var item in sellOrderRequests)
            {
                expectedSellOrderList.Add(await _stocksService.CreateSellOrder(item));
            }

            List<SellOrderResponse> actualSellOrdersList = await _stocksService.GetSellOrders();

            foreach (var item in expectedSellOrderList)
            {
                Assert.Contains(item, actualSellOrdersList);
            }
        }

        #endregion
    }
}