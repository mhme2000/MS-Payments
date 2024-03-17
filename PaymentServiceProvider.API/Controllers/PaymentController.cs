using Microsoft.AspNetCore.Mvc;
using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;

namespace PaymentServiceProvider.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IUpdateStatusPaymentUseCase updateStatusPaymentUseCase, ICreateTransactionUseCase createTransactionUseCase, IGetPaymentByOrderIdUseCase getPaymentByOrderIdUseCase) : ControllerBase
{
    private readonly IUpdateStatusPaymentUseCase _updateStatusPaymentUseCase = updateStatusPaymentUseCase;
    private readonly ICreateTransactionUseCase _createTransactionUseCase = createTransactionUseCase;
    private readonly IGetPaymentByOrderIdUseCase _getPaymentByOrderIdUseCase = getPaymentByOrderIdUseCase;

    [HttpGet("findByOrderId")]
    public IActionResult GetPaymentByOrderId([FromQuery] Guid orderId)
    {
        var result = _getPaymentByOrderIdUseCase.Execute(orderId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPatch]
    public IActionResult UpdateStatusPayment([FromBody] UpdateStatusPaymentDto dto)
    {
        var result = _updateStatusPaymentUseCase.Execute(dto);
        if (result == null) return NotFound();
        return NoContent();
    }

    [HttpPost]
    public IActionResult CreatePayment([FromBody] ConsumerModel dto)
    {
        var result = _createTransactionUseCase.Execute(dto);
        if (result == null) return BadRequest();
        return Created();
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(DateTime.Now);
    }
}
