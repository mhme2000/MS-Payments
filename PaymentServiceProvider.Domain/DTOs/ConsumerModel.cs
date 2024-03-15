namespace PaymentServiceProvider.Domain.DTOs;

public class ConsumerModel
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public Guid OrderId { get; set; }
}