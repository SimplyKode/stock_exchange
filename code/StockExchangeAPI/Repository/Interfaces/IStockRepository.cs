using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStockRepository
    {
        public Task<StockPriceDto> GetStockPrice(string stockSymbol);
        public Task<List<StockPriceDto>> GetStockPrice(List<string> stockSymbolList);
        public Task<List<StockPriceDto>> GetStockPrice(int pageNumber, int pageSize);
    }
}
