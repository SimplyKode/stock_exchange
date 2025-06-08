using DataAccess.Models;
using Repository.Interfaces;

namespace Repository.RepoClasses
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DevelopmentContext _context;

        public TransactionRepository(DevelopmentContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessTransaction(StockTransaction transaction)
        {
            try
            {
                await _context.StockTransactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            
            return true;
        }        
    }
}
