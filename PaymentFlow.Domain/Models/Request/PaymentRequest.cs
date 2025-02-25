namespace PaymentFlow.Domain.Models.Request
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } // "Debito" ou "Credito"
    }
}
