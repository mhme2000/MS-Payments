using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Domain.Interfaces;

public interface IPaymentRepository
{
    void Update(Payment payment);
    Payment? FindByOrderId(Guid orderId);
}