using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using StockExchangeAPI.Interfaces;

namespace StockExchangeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly ITransactionService _transactionService;        

        public TransactionController(ILoggerService logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;        
        }

        [HttpPost("process", Name = "ProcessTransaction")]
        public async Task<IActionResult> GetPriceForList([FromBody] StockTransaction transaction)
        {
            try
            {
                if (transaction == null || transaction.Stocksymbol == null || transaction.Quantity == null || transaction.Unitprice == null)
                {
                    return BadRequest("Supplied Transaction is null.");
                }
                
                var result = await _transactionService.ProcessTransaction(transaction);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return StatusCode(500, "An error occurred while processing the transaction.");
            }
        }
    }
}