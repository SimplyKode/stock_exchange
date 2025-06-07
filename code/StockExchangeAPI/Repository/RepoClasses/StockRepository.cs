using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepoClasses
{
    public class StockRepository : IStockRepository
    {
        private readonly DevelopmentContext _context;

        public StockRepository(DevelopmentContext context)
        {
            _context = context;
        }
        public async Task<StockPriceDto> GetStockPrice(string stockSymbol)
        {
            var avgPrice = await _context.StockTransactions
                                .Where(t => t.Stocksymbol.ToLower() == stockSymbol.ToLower())
                                .AverageAsync(t => t.Unitprice);

            if (avgPrice == null)
            {
                return null;
            }

            return new StockPriceDto
            {
                StockSymbol = stockSymbol.ToUpper(),
                AveragePrice = avgPrice.Value
            };
        }

        public async Task<List<StockPriceDto>> GetStockPrice(List<string> stockSymbolList)
        {
            List<StockPriceDto> result = new List<StockPriceDto>();

            try
            {
                var symbolList = stockSymbolList.Select(s => s.ToLower()).ToList();

                result = await _context.StockTransactions
                                    .Where(t => symbolList.Contains(t.Stocksymbol.ToLower()))
                                    .GroupBy(t => t.Stocksymbol)
                                    .Select(g => new StockPriceDto
                                    {
                                        StockSymbol = g.Key,
                                        AveragePrice = g.Average(t => t.Unitprice)
                                    }).ToListAsync();
            }
            catch 
            {
                throw;
            }

            return result;
            
        }

        public async Task<List<StockPriceDto>> GetStockPrice(int pageNumber, int pageSize)
        {
            List<StockPriceDto> result = new List<StockPriceDto>();

            try
            {
                result = await _context.StockTransactions
                                    .GroupBy(t => t.Stocksymbol)
                                    .OrderBy(g => g.Key)
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(g => new StockPriceDto
                                    {
                                        StockSymbol = g.Key,
                                        AveragePrice = g.Average(t => t.Unitprice)
                                    })
                                    .ToListAsync();
            }

            catch
            {
                throw;
            }

            return result;            
        }
    }
}
