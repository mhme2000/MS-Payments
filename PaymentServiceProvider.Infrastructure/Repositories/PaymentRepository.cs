using PaymentServiceProvider.Domain.Entities;
using PaymentServiceProvider.Domain.Interfaces;
using PaymentServiceProvider.Infrastructure.Contexts;

namespace PaymentServiceProvider.Infrastructure.Repositories;

public class PaymentRepository(PaymentContext context) : IPaymentRepository
{
    private readonly PaymentContext _context = context;

    public void Update(Payment payment)
    {
        _context.Payment.Update(payment);
        _context.SaveChanges();
    }

    public Payment? FindByOrderId(Guid orderId)
    {
        return _context.Payment.FirstOrDefault(t => t.OrderId == orderId);
    }
}
