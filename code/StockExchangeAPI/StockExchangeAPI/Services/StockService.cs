using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using StockExchangeAPI.Interfaces;

namespace StockExchangeAPI.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepo;

        public StockService(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }
        public async Task<decimal?> GetStockPrice(string stockSymbol)
        {
            StockPriceDto dto = new StockPriceDto();
            try
            {
                if (string.IsNullOrEmpty(stockSymbol))
                {
                    return null;
                }

                dto = await _stockRepo.GetStockPrice(stockSymbol);

                if (dto == null)
                {
                    return null;
                }

                return dto.AveragePrice;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Dictionary<string, decimal?>> GetStockPrice(List<string> stockSymbolList)
        {
            Dictionary<string, decimal?> result = new Dictionary<string, decimal?>();
            try
            {
                if (stockSymbolList.Count == 0)
                {
                    return result;
                }

                var stockPriceDtoList = await _stockRepo.GetStockPrice(stockSymbolList);

                if (stockPriceDtoList.Count == 0)
                {
                    return result;
                }

                foreach (var symbol in stockSymbolList)
                {
                    var matchingStock = stockPriceDtoList.Where(s => s.StockSymbol.ToLower() == symbol.ToLower()).ToList();

                    if (matchingStock.Count == 0 || result.ContainsKey(symbol))
                    {
                        result.Add(symbol, null);
                        continue;
                    }

                    var averagePrice = matchingStock.Average(x => x.AveragePrice);                    
                    result.Add(symbol, averagePrice);                    
                }
            }
            catch
            {
                throw;
            }

            return result;
        }
        public async Task<Dictionary<string, decimal?>> GetStockPrice(int pageNumber, int pageSize)
        {
            Dictionary<string, decimal?> result = new Dictionary<string, decimal?>();

            try
            {
                //Set default page number
                if (pageNumber <= 0)
                {
                    pageNumber = 1;
                }
                //Set default page size
                if (pageSize <= 0)
                {
                    pageSize = 10;
                }

                var stockList = await _stockRepo.GetStockPrice(pageNumber, pageSize);

                stockList.ForEach(s => { result.Add(s.StockSymbol, s.AveragePrice); });
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
