using System.Text.Json.Serialization;

namespace PaymentServiceProvider.Domain.Entities;

public class Transaction : Entity
{
    private const double FeeDebitCard = 0.03;
    private const double FeeCreditCard = 0.05;

    public Transaction(double amount, string description, PaymentMethod paymentMethod, uint numberOfInstallments, string cardNumber, string cardName, DateTime cardExpireDate, string cardCvv)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Description = description;
        PaymentMethod = paymentMethod;
        NumberOfInstallments = numberOfInstallments;
        CardNumber = cardNumber;
        CardName = cardName;
        CardExpireDate = cardExpireDate;
        CardCvv = cardCvv;
        Payables = new List<Payable>() { };
        
        CreatePayables(amount, paymentMethod, numberOfInstallments);
    }
        
    public double Amount { get; private set; }
    public string Description { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public uint NumberOfInstallments { get; private set; }
    public string CardNumber { get; private set; }
    public string CardName { get; private set; }
    public DateTime CardExpireDate { get; private set; }
    public string CardCvv { get; private set; }
    [JsonIgnore]
    public ICollection<Payable> Payables { get; private set; }

    public double TotalWithoutFees()
    {
        return Amount - (Amount * Fee());
    }
    
    public double TotalAvailableWithoutFees()
    {
        return Payables.Where(t=> t.Status == StatusPayable.Payed).Sum(t => t.Amount);
    }

    private double Fee()
    {
        return PaymentMethod == PaymentMethod.CreditCard ? FeeCreditCard : FeeDebitCard;
    }
    private void CreatePayables(double amount, PaymentMethod paymentMethod, uint numberOfInstallments)
    {
        for (var i = 0; i < numberOfInstallments; i++)
        {
            var payable = paymentMethod == PaymentMethod.CreditCard
                ? CreatePayableByCreditCard(amount/numberOfInstallments)
                : CreatePayableByDebitCard(amount);
            Payables.Add(payable);
        }
    }

    private Payable CreatePayableByCreditCard(double amount)
    {
        var feePayable = Fee() * amount;
        return new Payable(amount, StatusPayable.WaitingFunds, DateTime.Now.AddDays(30) ,feePayable, Id);
    }
    
    private Payable CreatePayableByDebitCard(double amount)
    {
        var feePayable = Fee() * amount;
        return new Payable(amount, StatusPayable.Payed, DateTime.Now ,feePayable, Id);
    }
}

public enum PaymentMethod
{
    DebitCard = 1,
    CreditCard = 2
}