using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.Interfaces;

namespace PaymentServiceProvider.Application.UseCases.Transaction;

public class GetListTransactionsUseCase : IGetListTransactionsUseCase
{
    private readonly ITransactionRepository _transactionRepository;

    public GetListTransactionsUseCase(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public List<Domain.Entities.Transaction> Execute(bool any)
    {
        var transactions = _transactionRepository.GetList();
        return transactions;
    }
}