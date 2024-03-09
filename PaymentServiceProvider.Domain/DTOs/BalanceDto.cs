namespace PaymentServiceProvider.Domain.DTOs;

public class BalanceDto
{
    public double BalanceAvailable { get; set; }
    public double BalanceWaitingFunds { get; set; }
}