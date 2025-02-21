using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentFlow.Domain.Services;

namespace PaymentFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceFinancialController : ControllerBase
    {
        private readonly IBalanceFinancialService _balanceFinancialService;
        private readonly ILogger<BalanceFinancialController> _logger;
        public BalanceFinancialController
        (
            IBalanceFinancialService balanceFinancialService, 
            ILogger<BalanceFinancialController> logger)
        {
            _balanceFinancialService = balanceFinancialService;
            _logger = logger;
        }

        /// <summary>
        /// Retorna o total de pagamentos realizados em um dia específico
        /// </summary>
        /// <param name="dailyDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDailyTotalAmountAsync(DateTime dailyDate)
        {
            var paymentResponse = await _balanceFinancialService.GetDailyTotalAmountAsync(dailyDate);

            return Ok(new { Message = "Pagamento realizado com sucesso!", paymentResponse });
        }

    }
}
