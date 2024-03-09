using System.Net;
using Microsoft.AspNetCore.Mvc;
using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Domain.DTOs;

namespace PaymentServiceProvider.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ICreateTransactionUseCase _createTransactionUseCase;
    private readonly IGetListTransactionsUseCase _getListTransactionsUseCase;
    public TransactionController(ICreateTransactionUseCase createTransactionUseCase, IGetListTransactionsUseCase getListTransactionsUseCase)
    {
        _createTransactionUseCase = createTransactionUseCase;
        _getListTransactionsUseCase = getListTransactionsUseCase;
    }

    [HttpGet]
    public IActionResult GetTransactions()
    {
        var transactions = _getListTransactionsUseCase.Execute(true);
        return Ok(transactions);
    }

    [HttpPost]
    public IActionResult CreateTransaction([FromBody] TransactionDto transactionDto)
    {
        var id = _createTransactionUseCase.Execute(transactionDto);
        Response.Headers.Location = $"/transaction/{id.ToString()}";
        return new ObjectResult(id.ToString()){StatusCode = (int)HttpStatusCode.Created};
    }
}
