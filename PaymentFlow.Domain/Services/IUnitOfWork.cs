namespace PaymentFlow.Domain.Services
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
