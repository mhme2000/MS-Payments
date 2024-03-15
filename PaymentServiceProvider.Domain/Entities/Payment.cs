namespace PaymentServiceProvider.Domain.Entities;

public class Payment : Entity
{
    public Payment(double amount, string? description, PaymentMethod paymentMethod, Guid orderId)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Description = description ?? "Descrição vazia";
        PaymentMethod = paymentMethod;
        PaymentStatus = PaymentStatus.AguardandoPagamento;
        OrderId = orderId;
    }

    public double Amount { get; private set; }
    public string Description { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public Guid OrderId { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }

    public void UpdateStatus(PaymentStatus status)
    {
        PaymentStatus = status;
    }
}

public enum PaymentMethod
{
    Pix = 1,
}

public enum PaymentStatus
{
    AguardandoPagamento = 1,
    Pago = 2,
    Cancelado = 3
}