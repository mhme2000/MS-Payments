using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Domain.Interfaces;

public interface IPaymentRepository
{
    void Create(Payment payment);
    void Update(Payment payment);
    Payment? FindByOrderId(Guid orderId);
}