using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Models.Request
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } // "Debito" ou "Credito"
    }
}
