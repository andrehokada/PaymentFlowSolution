namespace PaymentFlow.Domain.Entities
{
    public class Payment
    {
        public Payment
        (
            decimal amount,
            string type
        )
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Type = type;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Type { get; private set; }
        public DateTime CreatedAt { get; private set; }


    }
}
