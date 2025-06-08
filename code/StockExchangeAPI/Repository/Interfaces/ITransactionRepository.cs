using DataAccess.Models;

namespace Repository.Interfaces
{
    public interface ITransactionRepository
    {        
        public Task<bool> ProcessTransaction(StockTransaction transaction);
    }
}
