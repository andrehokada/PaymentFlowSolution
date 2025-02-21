using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; }
        public decimal Amount { get; }
        public string Type { get; } // "Debito" ou "Credito"
        public DateTime CreatedAt { get; }

        public Payment(decimal amount, string type)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            Type = type;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
