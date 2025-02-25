using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public async Task AddPaymentAsync(Payment payment)
    {
        try
        {
            _logger.LogInformation($"Inserindo pagamento de {payment.Amount} via {payment.Type}");
            await _context.Payments.AddAsync(payment);
        }
        catch (SqlException sqlex)
        {
            _logger.LogError($"Erro ao inserir pagamento - Message: {sqlex.Message}");
            throw new Exception(sqlex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao inserir pagamento - Message: {ex.Message}");
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Payment>?> GetPaymentsByDailyDateAsync(DateTime dailyDate)
    {
        try
        {
            _logger.LogInformation($"Buscando pagamentos consolidados do dia: {dailyDate}");
            return await _context.Payments
                                 .Where(x => x.CreatedAt.Date == dailyDate.Date)
                                 .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao buscar pagamentos - Message: {ex.Message}");
            throw new Exception(ex.Message);
        }

    }
}

