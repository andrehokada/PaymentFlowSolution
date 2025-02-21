using Microsoft.Extensions.Logging;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Models.Request;
using PaymentFlow.Domain.Models.Response;
using PaymentFlow.Domain.Repositories;
using PaymentFlow.Domain.Services;

namespace PaymentFlow.Application.Services;

public class PaymentService : IPaymentService
{
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentService> _logger;
    public PaymentService
    (
        IPaymentRepository paymentRepository,
        ILogger<PaymentService> logger
    )
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
    }

    public async Task<int> AddPaymentAsync(PaymentRequest paymentRequest)
    {
        _logger.LogInformation($"Inserindo pagamento de {paymentRequest.Amount} via {paymentRequest.Type} - camada Service");
        var payment = new Payment(paymentRequest.Amount, paymentRequest.Type);
        var paymentId = await _paymentRepository.AddPaymentAsync(payment);
        
        return paymentId;
    }
    public async Task<PaymentResponse> GetPaymentByIdAsync(Guid id)
    {
        _logger.LogInformation($"Buscando pagamento por ID: {id} - camada Service");
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        return new PaymentResponse(payment.Id, payment.Amount, payment.Type, payment.CreatedAt);
    }
}
