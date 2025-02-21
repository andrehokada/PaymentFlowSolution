using Microsoft.Extensions.Logging;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Repositories;

namespace PaymentFlow.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;
    private readonly ILogger<PaymentRepository> _logger;
    public PaymentRepository
    (
        PaymentDbContext context, 
        ILogger<PaymentRepository> logger
    )
    {
        _context = context;
        _logger = logger;
    }
    public async Task<int> AddPaymentAsync(Payment payment)
    {
        _logger.LogInformation($"Inserindo pagamento de {payment.Amount} via {payment.Type}");
        await _context.Payments.AddAsync(payment);
        return await _context.SaveChangesAsync();
    }
    public async Task<Payment?> GetPaymentByIdAsync(Guid id)
    {
        _logger.LogInformation($"Buscando pagamento por ID: {id}");
        return await _context.Payments.FindAsync(id);
    }
}

