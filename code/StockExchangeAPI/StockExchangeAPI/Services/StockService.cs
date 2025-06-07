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
            try
            {
                if (string.IsNullOrEmpty(stockSymbol))
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }

            return await _stockRepo.GetStockPrice(stockSymbol);

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

                var stockList = await _stockRepo.GetStockPrice(stockSymbolList);

                if (stockList.Count == 0)
                {
                    return result;
                }

                foreach (var symbol in stockSymbolList)
                {
                    var matchingStock = stockList.Where(s => s.Symbol.ToLower() == symbol.ToLower()).FirstOrDefault();

                    if (result.ContainsKey(symbol))
                    {
                        continue;
                    }
                    if (matchingStock != null)
                    {
                        result.Add(matchingStock.Symbol, matchingStock.Price);
                    }
                    else
                    {
                        result.Add(symbol, null);
                    }
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
                stockList.ForEach(s => { result.Add(s.Symbol, s.Price); });
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
