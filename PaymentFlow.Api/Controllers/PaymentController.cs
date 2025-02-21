using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Models.Request;
using PaymentFlow.Domain.Services;

namespace PaymentFlow.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController
    (
        IPaymentService paymentService, 
        ILogger<PaymentsController> logger
    )
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpPost]
    [Authorize] // Garante que só usuários autenticados possam criar pagamentos
    public async Task<IActionResult> AddPaymentAsync([FromBody] PaymentRequest paymentRequest)
    {
        if (paymentRequest.Amount <= 0 || (paymentRequest.Type != "Debito" && paymentRequest.Type != "Credito"))
        {
            _logger.LogWarning("Tipo de pagamento inválido ou valor menor que zero.");
            return BadRequest("Tipo de pagamento inválido ou valor menor que zero.");
        }

        var paymentResponse = await _paymentService.AddPaymentAsync(paymentRequest);

        return Ok(new { Message = "Pagamento realizado com sucesso!", paymentResponse });
    }
}
