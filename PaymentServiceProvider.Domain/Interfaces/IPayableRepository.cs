using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Domain.Interfaces;

public interface IPayableRepository
{
    double GetBalanceAvailable();
    double GetBalanceWaitingFunds();
    List<Payable> GetPayablesByDate(DateTime date);
    void UpdateBatch(IEnumerable<Payable> payables);
}