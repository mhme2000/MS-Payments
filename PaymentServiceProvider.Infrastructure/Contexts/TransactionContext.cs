using Microsoft.EntityFrameworkCore;
using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Infrastructure.Contexts;

public class TransactionContext : DbContext
{
    public TransactionContext(DbContextOptions<TransactionContext> options): base(options){}
    public DbSet<Transaction>? Transaction { get; set; }
    public DbSet<Payable>? Payable { get; set; }
}