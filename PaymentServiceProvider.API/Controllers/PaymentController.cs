using Microsoft.AspNetCore.Mvc;
using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;

namespace PaymentServiceProvider.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IUpdateStatusPaymentUseCase updateStatusPaymentUseCase) : ControllerBase
{
    private readonly IUpdateStatusPaymentUseCase _updateStatusPaymentUseCase = updateStatusPaymentUseCase;

    [HttpPatch]
    public IActionResult UpdateStatusPayment([FromBody] UpdateStatusPaymentDto dto)
    {
        var result = _updateStatusPaymentUseCase.Execute(dto);
        if (result == null) return NotFound();
        return NoContent();
    }
}
