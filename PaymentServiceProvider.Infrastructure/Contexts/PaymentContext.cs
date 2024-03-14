using Microsoft.EntityFrameworkCore;
using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Infrastructure.Contexts;

public class PaymentContext(DbContextOptions<PaymentContext> options) : DbContext(options)
{
    public DbSet<Payment> Payment { get; set; } = null!;
}