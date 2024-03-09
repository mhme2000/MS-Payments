using PaymentServiceProvider.Application.Interfaces.Payable;
using PaymentServiceProvider.Domain.DTOs;
using PaymentServiceProvider.Domain.Interfaces;

namespace PaymentServiceProvider.Application.UseCases.Payable;

public class GetBalancesUseCase : IGetBalancesUseCase
{
    private readonly IPayableRepository _payableRepository;

    public GetBalancesUseCase(IPayableRepository payableRepository)
    {
        _payableRepository = payableRepository;
    }

    public BalanceDto Execute(bool any)
    {
        var balanceAvailable = _payableRepository.GetBalanceAvailable();
        var balanceWaitingFunds = _payableRepository.GetBalanceWaitingFunds();
        var balanceDto = new BalanceDto{BalanceAvailable = balanceAvailable, BalanceWaitingFunds = balanceWaitingFunds};
        return balanceDto;
    }
}