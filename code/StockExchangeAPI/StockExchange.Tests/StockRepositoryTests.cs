using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.RepoClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class StockRepositoryTests
{
    private DevelopmentContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DevelopmentContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid().ToString())
            .Options;

        var context = new DevelopmentContext(options);

        context.StockTransactions.AddRange(new List<StockTransaction>
        {
            new StockTransaction { Stocksymbol = "AAPL", Unitprice = 150 },
            new StockTransaction { Stocksymbol = "AAPL", Unitprice = 200 },
            new StockTransaction { Stocksymbol = "GOOGL", Unitprice = 1000 },
            new StockTransaction { Stocksymbol = "GOOGL", Unitprice = 1100 },
            new StockTransaction { Stocksymbol = "MSFT", Unitprice = 300 },
        });

        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task GetStockPrice_WithValidSymbol_ReturnsCorrectAverage()
    {
        var context = CreateInMemoryContext();
        var repo = new StockRepository(context);

        var result = await repo.GetStockPrice("AAPL");

        Assert.NotNull(result);
        Assert.Equal("AAPL", result.StockSymbol);
        Assert.Equal(175, result.AveragePrice); // (150+200)/2
    }

    [Fact]
    public async Task GetStockPrice_WithInvalidSymbol_ReturnsNull()
    {
        var context = CreateInMemoryContext();
        var repo = new StockRepository(context);

        var result = await repo.GetStockPrice("TSLA");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetStockPrice_WithSymbolList_ReturnsCorrectAverages()
    {
        var context = CreateInMemoryContext();
        var repo = new StockRepository(context);

        var symbols = new List<string> { "AAPL", "GOOGL" };
        var result = await repo.GetStockPrice(symbols);

        Assert.Equal(2, result.Count);

        var aapl = result.FirstOrDefault(r => r.StockSymbol.ToLower() == "aapl");
        Assert.NotNull(aapl);
        Assert.Equal(175, aapl.AveragePrice);

        var googl = result.FirstOrDefault(r => r.StockSymbol.ToLower() == "googl");
        Assert.NotNull(googl);
        Assert.Equal(1050, googl.AveragePrice); // (1000+1100)/2
    }

    [Fact]
    public async Task GetStockPrice_WithPaging_ReturnsCorrectSubset()
    {
        var context = CreateInMemoryContext();
        var repo = new StockRepository(context);

        var result = await repo.GetStockPrice(pageNumber: 1, pageSize: 2);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.StockSymbol.ToLower() == "aapl");
        Assert.Contains(result, r => r.StockSymbol.ToLower() == "googl");
    }
}
