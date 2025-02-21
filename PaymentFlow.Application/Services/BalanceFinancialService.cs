using Microsoft.Extensions.Logging;
using PaymentFlow.Domain.Repositories;
using PaymentFlow.Domain.Services;

namespace PaymentFlow.Application.Services
{
    public class BalanceFinancialService : IBalanceFinancialService
    {
        private readonly IBalanceFinancialRepository _balanceFinancialRepository;
        private readonly ILogger<PaymentService> _logger;

        public BalanceFinancialService(IBalanceFinancialRepository balanceFinancialRepository, ILogger<PaymentService> logger)
        {
            _balanceFinancialRepository = balanceFinancialRepository;
            _logger = logger;
        }
        public async Task<decimal> GetDailyTotalAmountAsync(DateTime dailyDate)
        {
            _logger.LogInformation("Calculando a soma dos pagamentos do dia.");
            return await _balanceFinancialRepository.GetDailyTotalAmountAsync(dailyDate);
        }
    }
}
