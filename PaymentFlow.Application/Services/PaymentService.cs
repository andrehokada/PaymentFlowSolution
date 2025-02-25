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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PaymentService> _logger;
    public PaymentService
    (
        IPaymentRepository paymentRepository,
        IUnitOfWork unitOfWork,
        ILogger<PaymentService> logger
    )
    {
        _paymentRepository = paymentRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task AddPaymentAsync(PaymentRequest paymentRequest)
    {
        try
        {
            _logger.LogInformation($"Inserindo pagamento de {paymentRequest.Amount} via {paymentRequest.Type} - camada Service");
            var payment = new Payment(paymentRequest.Amount, paymentRequest.Type);

            await _paymentRepository.AddPaymentAsync(payment);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao inserir pagamento - Message: {ex.Message}");
            throw ex;
        }
    }

    public async Task<IEnumerable<PaymentResponseCustom>?> GetPaymentsByDailyDateAsync(DateTime dailyDate)
    {
        try
        {
            _logger.LogInformation($"Buscando pagamentos consolidados do dia: {dailyDate} - camada Service");
            var payments = await _paymentRepository.GetPaymentsByDailyDateAsync(dailyDate);

            if (payments == null)
            {
                _logger.LogWarning($"Nenhum pagamento encontrado para a data: {dailyDate}");
                return [];
            }

            var totalAmount = payments.Sum(x => x.Amount);
            var paymentResponses = new PaymentResponseCustom(totalAmount, payments.Select(x => new PaymentResponse(x.Id, x.Amount, x.Type, x.CreatedAt)));
            return [paymentResponses];
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar pagamentos - Message: {ex.Message}");
            throw ex;
        }
    }
}
