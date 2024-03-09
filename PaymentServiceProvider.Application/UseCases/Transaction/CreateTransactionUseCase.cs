using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;
using PaymentServiceProvider.Domain.Interfaces;

namespace PaymentServiceProvider.Application.UseCases.Transaction;

public class CreateTransactionUseCase : ICreateTransactionUseCase
{
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionUseCase(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public Guid Execute(TransactionDto transactionDto)
    {
        var transaction = new Domain.Entities.Transaction(transactionDto.Amount, transactionDto.Description, transactionDto.PaymentMethod, transactionDto.NumberOfInstallments, transactionDto.CardNumber, transactionDto.CardName, transactionDto.CardExpireDate, transactionDto.CardCvv);
        var id = _transactionRepository.Create(transaction);
        return id;
    }
}
