namespace PaymentFlow.Domain.Models.Response
{
    public class PaymentResponse
    {
        public Guid Id { get; }
        public decimal Amount { get; }
        public string Type { get; }
        public DateTime CreatedAt { get; }
        public PaymentResponse
        (
            Guid id,
            decimal amount,
            string type,
            DateTime createdAt
        )
        {
            Id = id;
            Amount = amount;
            Type = type;
            CreatedAt = createdAt;
        }
    }
}
