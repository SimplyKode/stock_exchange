using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.RepoClasses;

public class TransactionRepositoryTests
{
    private DevelopmentContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DevelopmentContext>()
            .UseInMemoryDatabase(databaseName: "TransactionDb_" + System.Guid.NewGuid().ToString())
            .Options;

        return new DevelopmentContext(options);
    }

    [Fact]
    public async Task ProcessTransaction_ValidTransaction_AddsToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new TransactionRepository(context);
        var transaction = new StockTransaction
        {
            Stocksymbol = "AAPL",
            Unitprice = 150
        };

        // Act
        var result = await repository.ProcessTransaction(transaction);

        // Assert
        Assert.True(result);

        var storedTransaction = await context.StockTransactions.FirstOrDefaultAsync(t => t.Stocksymbol == "AAPL");
        Assert.NotNull(storedTransaction);
        Assert.Equal(150, storedTransaction.Unitprice);
    }

    [Fact]
    public async Task ProcessTransaction_NullTransaction_ThrowsException()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new TransactionRepository(context);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => repository.ProcessTransaction(null));
    }

    [Fact]
    public async Task ProcessTransaction_MultipleTransactions_StoresAll()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new TransactionRepository(context);

        var transactions = new[]
        {
            new StockTransaction { Stocksymbol = "AAPL", Unitprice = 150 },
            new StockTransaction { Stocksymbol = "GOOGL", Unitprice = 1000 }
        };

        // Act
        foreach (var tx in transactions)
        {
            await repository.ProcessTransaction(tx);
        }

        // Assert
        var allTransactions = await context.StockTransactions.ToListAsync();
        Assert.Equal(2, allTransactions.Count);
        Assert.Contains(allTransactions, t => t.Stocksymbol == "AAPL");
        Assert.Contains(allTransactions, t => t.Stocksymbol == "GOOGL");
    }
}
