using DataAccess.Models;
using Moq;
using Repository.Interfaces;
using StockExchangeAPI.Services;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _mockTransactionRepo;
    private readonly TransactionService _transactionService;

    public TransactionServiceTests()
    {
        _mockTransactionRepo = new Mock<ITransactionRepository>();
        _transactionService = new TransactionService(_mockTransactionRepo.Object);
    }

    [Fact]
    public async Task ProcessTransaction_WithValidTransaction_ReturnsTrue()
    {
        // Arrange
        var transaction = new StockTransaction { Stocksymbol = "AAPL", Unitprice = 150 };
        _mockTransactionRepo
            .Setup(repo => repo.ProcessTransaction(transaction))
            .ReturnsAsync(true);

        // Act
        var result = await _transactionService.ProcessTransaction(transaction);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ProcessTransaction_RepositoryReturnsFalse_ReturnsFalse()
    {
        // Arrange
        var transaction = new StockTransaction { Stocksymbol = "GOOG", Unitprice = 1000 };
        _mockTransactionRepo
            .Setup(repo => repo.ProcessTransaction(transaction))
            .ReturnsAsync(false);

        // Act
        var result = await _transactionService.ProcessTransaction(transaction);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ProcessTransaction_PropagatesRepositoryException()
    {
        // Arrange
        var transaction = new StockTransaction { Stocksymbol = "MSFT", Unitprice = 300 };
        _mockTransactionRepo
            .Setup(repo => repo.ProcessTransaction(transaction))
            .ThrowsAsync(new System.Exception("Repository error"));

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(() => _transactionService.ProcessTransaction(transaction));
    }
}
