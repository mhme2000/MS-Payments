using PaymentServiceProvider.Domain.DTOs;

namespace PaymentServiceProvider.Application.Interfaces.Transaction;

public interface IUpdateStatusPaymentUseCase : IUseCase<object?, UpdateStatusPaymentDto>
{
}