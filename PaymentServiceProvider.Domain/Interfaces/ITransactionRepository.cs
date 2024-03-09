using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Domain.Interfaces;

public interface ITransactionRepository
{
    Guid Create(Transaction transaction);
    List<Transaction> GetList();
}