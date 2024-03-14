using System.ComponentModel.DataAnnotations;
using PaymentServiceProvider.Domain.Entities;

namespace PaymentServiceProvider.Domain.DTOs;

public class UpdateStatusPaymentDto
{
    [Required] public Guid OrderId { get; set; }   
    [Required] public PaymentStatus Status { get; set; }
}