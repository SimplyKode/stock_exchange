using DataAccess.Models;
using Moq;
using Repository.Interfaces;
using StockExchangeAPI.Services;

public class StockServiceTests
{
    private readonly Mock<IStockRepository> _mockStockRepo;
    private readonly StockService _stockService;

    public StockServiceTests()
    {
        _mockStockRepo = new Mock<IStockRepository>();
        _stockService = new StockService(_mockStockRepo.Object);
    }

    [Fact]
    public async Task GetStockPrice_WithValidSymbol_ReturnsAveragePrice()
    {
        // Arrange
        var stockSymbol = "AAPL";
        var expectedPrice = 150m;

        _mockStockRepo
            .Setup(repo => repo.GetStockPrice(stockSymbol))
            .ReturnsAsync(new StockPriceDto { StockSymbol = stockSymbol, AveragePrice = expectedPrice });

        // Act
        var result = await _stockService.GetStockPrice(stockSymbol);

        // Assert
        Assert.Equal(expectedPrice, result);
    }

    [Fact]
    public async Task GetStockPrice_WithInvalidSymbol_ReturnsNull()
    {
        // Arrange
        var stockSymbol = "TSLA";

        _mockStockRepo
            .Setup(repo => repo.GetStockPrice(stockSymbol))
            .ReturnsAsync((StockPriceDto)null);

        // Act
        var result = await _stockService.GetStockPrice(stockSymbol);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetStockPrice_WithNullOrEmptySymbol_ReturnsNull()
    {
        var result1 = await _stockService.GetStockPrice((string)null);
        var result2 = await _stockService.GetStockPrice("");

        Assert.Null(result1);
        Assert.Null(result2);
    }

    [Fact]
    public async Task GetStockPrice_WithSymbolList_ReturnsCorrectDictionary()
    {
        // Arrange
        var symbols = new List<string> { "AAPL", "GOOG" };
        var stockDtos = new List<StockPriceDto>
        {
            new StockPriceDto { StockSymbol = "AAPL", AveragePrice = 150 },
            new StockPriceDto { StockSymbol = "GOOG", AveragePrice = 1000 }
        };

        _mockStockRepo
            .Setup(repo => repo.GetStockPrice(symbols))
            .ReturnsAsync(stockDtos);

        // Act
        var result = await _stockService.GetStockPrice(symbols);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(150, result["AAPL"]);
        Assert.Equal(1000, result["GOOG"]);
    }

    [Fact]
    public async Task GetStockPrice_WithEmptySymbolList_ReturnsEmptyDictionary()
    {
        // Arrange
        var symbols = new List<string>();

        // Act
        var result = await _stockService.GetStockPrice(symbols);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPrice_WithPartialMatches_ReturnsNullForMissingSymbols()
    {
        // Arrange
        var symbols = new List<string> { "AAPL", "MSFT" };
        var stockDtos = new List<StockPriceDto>
        {
            new StockPriceDto { StockSymbol = "AAPL", AveragePrice = 150 }
        };

        _mockStockRepo
            .Setup(repo => repo.GetStockPrice(symbols))
            .ReturnsAsync(stockDtos);

        // Act
        var result = await _stockService.GetStockPrice(symbols);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(150, result["AAPL"]);
        Assert.Null(result["MSFT"]);
    }

    [Fact]
    public async Task GetStockPrice_WithPaging_ReturnsCorrectSubset()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 2;

        var stockDtos = new List<StockPriceDto>
        {
            new StockPriceDto { StockSymbol = "AAPL", AveragePrice = 150 },
            new StockPriceDto { StockSymbol = "GOOGL", AveragePrice = 1000 }
        };

        _mockStockRepo
            .Setup(repo => repo.GetStockPrice(pageNumber, pageSize))
            .ReturnsAsync(stockDtos);

        // Act
        var result = await _stockService.GetStockPrice(pageNumber, pageSize);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(150, result["AAPL"]);
        Assert.Equal(1000, result["GOOGL"]);
    }

    [Fact]
    public async Task GetStockPrice_PropagatesExceptions()
    {
        // Arrange
        var stockSymbol = "AAPL";
        _mockStockRepo
            .Setup(repo => repo.GetStockPrice(stockSymbol))
            .ThrowsAsync(new System.Exception("Test exception"));

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(() => _stockService.GetStockPrice(stockSymbol));
    }
}
