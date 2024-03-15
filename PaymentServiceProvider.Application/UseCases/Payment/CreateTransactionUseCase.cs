using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;
using PaymentServiceProvider.Domain.Entities;
using PaymentServiceProvider.Domain.Interfaces;

namespace PaymentServiceProvider.Application.UseCases.Transaction;

public class CreateTransactionUseCase(IPaymentRepository paymentRepository) : ICreateTransactionUseCase
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    public object? Execute(ConsumerModel dto)
    {
        var payment = _paymentRepository.FindByOrderId(dto.OrderId);
        if (payment != null) return null;
        try
        {
            var newPayment = new Payment((double)dto.Amount, dto.Description, PaymentMethod.Pix, dto.OrderId);
            _paymentRepository.Create(newPayment);
            return new object();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
            return null;
        }
    }
}
