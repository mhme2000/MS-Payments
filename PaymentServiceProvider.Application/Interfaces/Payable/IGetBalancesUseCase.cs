using PaymentServiceProvider.Domain.DTOs;

namespace PaymentServiceProvider.Application.Interfaces.Payable;

public interface IGetBalancesUseCase : IUseCase<BalanceDto, bool>
{
}