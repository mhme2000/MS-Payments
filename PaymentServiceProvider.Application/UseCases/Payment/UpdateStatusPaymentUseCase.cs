using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;
using PaymentServiceProvider.Domain.Entities;
using PaymentServiceProvider.Domain.Interfaces;
using PaymentServiceProvider.Producer;
using System.Transactions;

namespace PaymentServiceProvider.Application.UseCases.Transaction;

public class UpdateStatusPaymentUseCase(IPaymentRepository paymentRepository) : IUpdateStatusPaymentUseCase
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private static readonly List<PaymentStatus> itemsForPublishMessage = [PaymentStatus.Pago, PaymentStatus.Cancelado];

    public object? Execute(UpdateStatusPaymentDto dto)
    {
        var payment = _paymentRepository.FindByOrderId(dto.OrderId);
        if (payment == null || payment.PaymentStatus == PaymentStatus.Cancelado || payment.PaymentStatus == PaymentStatus.Pago) return null;
        using TransactionScope scope = new();
        try
        {
            payment.UpdateStatus(dto.Status);
            _paymentRepository.Update(payment);
            if (itemsForPublishMessage.Contains(dto.Status))
                RabbitProducer.PublishMessage(new { dto.OrderId, Email = "teste@teste.com" }, dto.Status);
            scope.Complete();
            return new object();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
            return null;
        }
    }
}
