using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Domain.Repositories;

public interface IPaymentRepository
{
    Task<int> AddPaymentAsync(Payment payment);
    Task<Payment?> GetPaymentByIdAsync(Guid id);
}
