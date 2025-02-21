using PaymentFlow.Domain.Models.Request;
using PaymentFlow.Domain.Models.Response;


namespace PaymentFlow.Domain.Services
{
    public interface IPaymentService
    {
        Task<int> AddPaymentAsync(PaymentRequest paymentRequest);
        Task<PaymentResponse> GetPaymentByIdAsync(Guid id);
    }
}
