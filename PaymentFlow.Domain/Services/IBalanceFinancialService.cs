using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Services
{
    public interface IBalanceFinancialService
    {
        Task<decimal> GetDailyTotalAmountAsync(DateTime dailyDate);
    }
}
