using Microsoft.EntityFrameworkCore;
using PaymentServiceProvider.Domain.Entities;
using PaymentServiceProvider.Domain.Interfaces;
using PaymentServiceProvider.Infrastructure.Contexts;

namespace PaymentServiceProvider.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly TransactionContext _context;

    public TransactionRepository(TransactionContext context)
    {
        _context = context;
    }
    public Guid Create(Transaction transaction)
    {
        _context.Transaction.Add(transaction);
        _context.SaveChanges();
        return transaction.Id;
    }

    public List<Transaction> GetList()
    {
        return _context.Transaction.AsNoTracking().ToList();
    }
}
