using DataAccess.Models;

namespace Repository.Interfaces
{
    public interface IStockRepository
    {
        public Task<StockPriceDto> GetStockPrice(string stockSymbol);
        public Task<List<StockPriceDto>> GetStockPrice(List<string> stockSymbolList);
        public Task<List<StockPriceDto>> GetStockPrice(int pageNumber, int pageSize);
    }
}
