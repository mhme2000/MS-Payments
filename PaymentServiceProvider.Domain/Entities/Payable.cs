using System.Text.Json.Serialization;

namespace PaymentServiceProvider.Domain.Entities;

public class Payable : Entity
{
    public Payable(){}
    public Payable(double amount, StatusPayable status, DateTime paymentDate, double fee, Guid transactionId)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Status = status;
        PaymentDate = paymentDate;
        Fee = fee;
        TransactionId = transactionId;
    }
    
    public double Amount { get; private set; }
    public StatusPayable Status { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public double Fee { get; private set; }
    public Guid TransactionId { get; private set; }
    [JsonIgnore]
    public Transaction? Transaction { get; private set; }

    public void UpdateStatusPayable(StatusPayable statusPayable)
    {
        Status = statusPayable;
    }
}

public enum StatusPayable
{
    Payed = 1,
    WaitingFunds = 2
}