using Microsoft.AspNetCore.Mvc;
using StockExchangeAPI.Interfaces;

namespace StockExchangeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IStockService _stockService;        

        public StockController(ILoggerService logger, IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;        
        }

        [HttpGet(Name = "getprice")]
        public async Task<IActionResult> GetStockPrice(string stockSymbol)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stockSymbol))
                {
                    return BadRequest("Stock symbol is required.");
                }

                var result = await _stockService.GetStockPrice(stockSymbol);

                if (result == null)
                {
                    return NotFound("Stock not found.");
                }

                return Ok(result); 
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return StatusCode(500, "An error occurred while retrieving the stock price.");
            }
        }

        [HttpPost("getpricelist", Name = "GetPriceForList")]
        public async Task<IActionResult> GetPriceForList([FromBody] List<string> symbolList)
        {
            try
            {
                if (symbolList == null || symbolList.Count == 0)
                {
                    return BadRequest("Symbol list is empty or NULL.");
                }

                var result = await _stockService.GetStockPrice(symbolList);

                if (result.Count == 0)
                {
                    return NotFound("No matching stocks found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return StatusCode(500, "An error occurred while retrieving the stock prices.");
            }
        }

        [HttpPost("getpriceall", Name = "GetPriceForAll")]
        public async Task<IActionResult> GetPriceForAll(int pageNumber, int pageSize)
        {
            try
            {
                var result = await _stockService.GetStockPrice(pageNumber, pageSize);

                if (result.Count == 0)
                {
                    return NotFound("No matching stocks found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return StatusCode(500, "An error occurred while retrieving the stock prices.");
            }
        }
    }
}