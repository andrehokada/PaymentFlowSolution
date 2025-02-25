using PaymentFlow.Domain.Services;
using PaymentFlow.Infrastructure;

namespace PaymentFlow.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentDbContext _context;

        public UnitOfWork(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
