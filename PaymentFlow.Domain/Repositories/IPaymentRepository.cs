using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Domain.Repositories;

public interface IPaymentRepository
{
    Task AddPaymentAsync(Payment payment);
    Task<IEnumerable<Payment>?> GetPaymentsByDailyDateAsync(DateTime dailyDate);
}
