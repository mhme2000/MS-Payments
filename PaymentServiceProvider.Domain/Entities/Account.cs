namespace PaymentServiceProvider.Domain.Entities;

public class Account : Entity
{
    public Account(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
    }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public ICollection<Transaction> Transactions { get; } = null!;

    public double BalanceTotal
    {
        get
        {
            return Transactions.Sum(t => t.TotalWithoutFees());
        }
    }
    public double BalanceAvailable
    {
        get
        {
            return Transactions.Sum(t => t.TotalAvailableWithoutFees());
        }
    }

}