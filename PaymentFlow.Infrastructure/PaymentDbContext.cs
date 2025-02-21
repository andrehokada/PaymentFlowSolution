using Microsoft.EntityFrameworkCore;
using PaymentFlow.Domain.Entities;

namespace PaymentFlow.Infrastructure
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>(static entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Amount).HasPrecision(18, 2).IsRequired();
                entity.Property(p => p.Type).HasMaxLength(10).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();
            });
        }
    }
}
