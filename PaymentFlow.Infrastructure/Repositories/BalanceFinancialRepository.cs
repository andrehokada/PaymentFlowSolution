using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentFlow.Domain.Repositories;

namespace PaymentFlow.Infrastructure.Repositories
{
    public class BalanceFinancialRepository : IBalanceFinancialRepository
    {
        private readonly PaymentDbContext _context;
        private readonly ILogger<PaymentRepository> _logger;
        public BalanceFinancialRepository
        (
            PaymentDbContext context,
            ILogger<PaymentRepository> logger
        )
        {
            _context = context;
            _logger = logger;
        }
        public async Task<decimal> GetDailyTotalAmountAsync(DateTime dailyDate)
        {
            _logger.LogInformation("Calculando a soma dos pagamentos do dia.");
            return await _context.Payments
                .Where(p => p.CreatedAt.Date == dailyDate)
                .SumAsync(p => p.Amount);
        }
    }
}
