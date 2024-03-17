using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.Interfaces;

namespace PaymentServiceProvider.Application.UseCases.Transaction;

public class GetPaymentByOrderIdUseCase(IPaymentRepository paymentRepository) : IGetPaymentByOrderIdUseCase
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    public object? Execute(Guid orderId)
    {
        var payment = _paymentRepository.FindByOrderId(orderId);
        if (payment == null) return null;
        return payment;
    }
}
