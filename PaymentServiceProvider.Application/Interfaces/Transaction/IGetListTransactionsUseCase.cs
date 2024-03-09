namespace PaymentServiceProvider.Application.Interfaces.Transaction;

public interface IGetListTransactionsUseCase : IUseCase<List<Domain.Entities.Transaction>, bool>
{
}