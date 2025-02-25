using PaymentFlow.Domain.Models.Request;
using PaymentFlow.Domain.Models.Response;


namespace PaymentFlow.Domain.Services
{
    public interface IPaymentService
    {
        Task AddPaymentAsync(PaymentRequest paymentRequest);
        Task<IEnumerable<PaymentResponseCustom>?> GetPaymentsByDailyDateAsync(DateTime dailyDate);
    }
}
