using PaymentServiceProvider.Domain.DTOs;

namespace PaymentServiceProvider.Application.Interfaces.Transaction;

public interface IUpdateStatusPaymentUseCase : IUseCase<object?, UpdateStatusPaymentDto>
{
}
public interface ICreateTransactionUseCase : IUseCase<object?, ConsumerModel>
{
}

public interface IGetPaymentByOrderIdUseCase : IUseCase<object?, Guid>
{
}