using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;
using PaymentServiceProvider.Domain.Interfaces;

namespace PaymentServiceProvider.Application.UseCases.Transaction;

public class UpdateStatusPaymentUseCase(IPaymentRepository paymentRepository) : IUpdateStatusPaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;

    public object? Execute(UpdateStatusPaymentDto dto)
    {
        var payment = _paymentRepository.FindByOrderId(dto.OrderId);
        if (payment == null) return null;
        payment.UpdateStatus(dto.Status);
        _paymentRepository.Update(payment);
        return new object();
    }
}
