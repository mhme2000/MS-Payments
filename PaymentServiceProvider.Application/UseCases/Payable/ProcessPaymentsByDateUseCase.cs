using PaymentServiceProvider.Application.Interfaces.Payable;
using PaymentServiceProvider.Domain.Entities;
using PaymentServiceProvider.Domain.Interfaces;

namespace PaymentServiceProvider.Application.UseCases.Payable;

public class ProcessPaymentsByDateUseCase : IProcessPaymentsByDateUseCase
{
    private readonly IPayableRepository _payableRepository;

    public ProcessPaymentsByDateUseCase(IPayableRepository payableRepository)
    {
        _payableRepository = payableRepository;
    }

    public bool Execute(DateTime date)
    {
        var payables = _payableRepository.GetPayablesByDate(date);
        foreach (var payable in payables)
        {
            payable.UpdateStatusPayable(StatusPayable.Payed);
        }
        _payableRepository.UpdateBatch(payables);
        return true;
    }
}