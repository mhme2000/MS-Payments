using Microsoft.AspNetCore.Mvc;
using PaymentServiceProvider.Application.Interfaces.Payable;

namespace PaymentServiceProvider.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PayableController : ControllerBase
{
    private readonly IGetBalancesUseCase _getBalancesUseCase;
    private readonly IProcessPaymentsByDateUseCase _processPaymentsByDateUseCase;

    public PayableController(IGetBalancesUseCase getBalancesUseCase, IProcessPaymentsByDateUseCase processPaymentsByDateUseCase)
    {
        _getBalancesUseCase = getBalancesUseCase;
        _processPaymentsByDateUseCase = processPaymentsByDateUseCase;
    }

    [HttpGet]
    public IActionResult GetBalances()
    {
        var balanceDto = _getBalancesUseCase.Execute(true);
        return Ok(balanceDto);
    }

    [HttpPut]
    public IActionResult ProcessPaymentByDate([FromQuery] DateTime date)
    {
        _processPaymentsByDateUseCase.Execute(date);
        return Ok();
    }
}