using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation;
using Vk.Schema;

namespace Vk.Api.Controllers;

[Route("vk/api/v1/[controller]")]
[ApiController]
public class AccountTransactionController : ControllerBase
    {
    private IMediator mediator;
    public AccountTransactionController(IMediator mediator)
        {
        this.mediator = mediator;
    }


    [HttpPost]
    [Authorize(Roles = "customer")]
    public async Task<ApiResponse<MoneyTransferResponse>> Post([FromBody] MoneyTransferRequest request)
    {
        var operation = new CreateMoneyTransfer(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByReferenceNumber/{referenceNumber}")]
    [Authorize(Roles = "admin,customer")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Post(string referenceNumber)
    {
        var operation = new GetMoneyTransferByReference(referenceNumber);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByAccountId/{accountId}")]
    [Authorize(Roles = "admin,customer")]
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Post(int accountId)
    {
        var operation = new GetMoneyTransferByAccountId(accountId);
        var result = await mediator.Send(operation);
        return result;
    }
}

