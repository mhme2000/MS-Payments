using System.ComponentModel.DataAnnotations;
using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Domain.DTOs;

public class TransactionDto
{
    [Required] public double Amount { get; set; }
    [Required] public string Description { get; set; } = null!;
    [Required] public PaymentMethod PaymentMethod { get; set; } 
    [Required] public uint NumberOfInstallments { get; set; }
    [Required] public string CardNumber { get; set; } = null!;
    [Required] public string CardName { get; set; } = null!;
    [Required] public DateTime CardExpireDate { get; set; } 
    [Required] public string CardCvv { get; set; } = null!;
}