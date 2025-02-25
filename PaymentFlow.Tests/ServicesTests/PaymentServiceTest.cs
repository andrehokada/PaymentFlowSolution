using Microsoft.Extensions.Logging;
using Moq;
using PaymentFlow.Application.Services;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Models.Request;
using PaymentFlow.Domain.Repositories;
using PaymentFlow.Domain.Services;

public class PaymentServiceTest
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<PaymentService>> _loggerMock;
    private readonly PaymentService _paymentService;

    public PaymentServiceTest()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<PaymentService>>();
        _paymentService = new PaymentService(_paymentRepositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task AddPaymentAsync_ShouldInsertPayment_Success()
    {
        var paymentRequest = new PaymentRequest { Amount = 100.50m, Type = "Credito" };

        await _paymentService.AddPaymentAsync(paymentRequest);

        _paymentRepositoryMock.Verify(repo => repo.AddPaymentAsync(It.IsAny<Payment>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task AddPaymentAsync_ShouldLogError_WhenExceptionThrown()
    {
        var paymentRequest = new PaymentRequest { Amount = 100.50m, Type = "Credito" };
        _paymentRepositoryMock.Setup(repo => repo.AddPaymentAsync(It.IsAny<Payment>())).ThrowsAsync(new Exception("Database error"));

        await Assert.ThrowsAsync<Exception>(() => _paymentService.AddPaymentAsync(paymentRequest));

        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Error,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Database error")),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
    }

    [Fact]
    public async Task GetPaymentsByDailyDateAsync_ShouldReturnPayments_Success()
    {
        var dailyDate = DateTime.Now;
        var payments = new List<Payment>
        {
            new Payment(100.50m, "Credito"),
            new Payment(200.75m, "Debito")
        };
        _paymentRepositoryMock.Setup(repo => repo.GetPaymentsByDailyDateAsync(dailyDate)).ReturnsAsync(payments);

        var result = await _paymentService.GetPaymentsByDailyDateAsync(dailyDate);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(301.25m, result.First().TotalCurrencyDaily);
    }

    [Fact]
    public async Task GetPaymentsByDailyDateAsync_ShouldReturnEmpty_WhenNoPaymentsFound()
    {
        var dailyDate = DateTime.Now;
        _paymentRepositoryMock.Setup(repo => repo.GetPaymentsByDailyDateAsync(dailyDate)).ReturnsAsync((IEnumerable<Payment>?)null);

        var result = await _paymentService.GetPaymentsByDailyDateAsync(dailyDate);

        Assert.Empty(result);
    }
}
