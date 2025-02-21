using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Repositories
{
    public interface IBalanceFinancialRepository
    {
        Task<decimal> GetDailyTotalAmountAsync(DateTime dailyDate);
    }
}
