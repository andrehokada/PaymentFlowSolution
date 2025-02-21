using Microsoft.Extensions.Logging;
using Moq;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Repositories;
using PaymentFlow.Infrastructure;
using PaymentFlow.Infrastructure.Repositories;

public class PaymentServiceTest
{
    private readonly Mock<IPaymentRepository> _accountRepositoryMock;

    public PaymentServiceTest()
    {
        _accountRepositoryMock = new Mock<IPaymentRepository>();
    }

    [Fact]
    public async Task AddPaymentAsync_ShouldInsertPayment()
    {
        var mockDbContext = new Mock<PaymentDbContext>();
        var mockLogger = new Mock<ILogger<PaymentRepository>>();
        var repository = new PaymentRepository(mockDbContext.Object, mockLogger.Object);

        var payment = new Payment(100.50m, "Credito");

        await repository.AddPaymentAsync(payment);

        mockDbContext.Verify(db => db.Payments.AddAsync(payment, It.IsAny<CancellationToken>()), Times.Once);
        mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetPaymentByIdAsync_ShouldReturnPayment()
    {
        var mockDbContext = new Mock<PaymentDbContext>();
        var mockLogger = new Mock<ILogger<PaymentRepository>>();
        var repository = new PaymentRepository(mockDbContext.Object, mockLogger.Object);

        var payment = await repository.GetPaymentByIdAsync(Guid.NewGuid());

        Assert.NotNull(payment);
    }
}
