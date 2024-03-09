using PaymentServiceProvider.Domain.DTOs;

namespace PaymentServiceProvider.Application.Interfaces.Transaction;

public interface ICreateTransactionUseCase : IUseCase<Guid, TransactionDto>
{
}