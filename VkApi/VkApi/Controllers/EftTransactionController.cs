//TODO :authorization and authentication

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation;
using Vk.Schema;

namespace VkApi.Controllers;


[Route("vk/api/v1/[controller]")]
[ApiController]
public class EftTransactionController : ControllerBase
{
    private IMediator mediator;

    public EftTransactionController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpPost]
    public async Task<ApiResponse<EftTransactionResponse>> Post([FromBody] EftTransactionRequest request)
    {
        var operation = new CreateEftTransaction(request);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByReferenceNumber/{referenceNumber}")]
    public async Task<ApiResponse<List<EftTransactionResponse>>> Post(string referenceNumber)
    {
        var operation = new GetEftTransactionByRefNoQuery(referenceNumber);
        var result = await mediator.Send(operation);
        return result;
    }

    [HttpGet("ByAccountId/{accountId}")]
    public async Task<ApiResponse<List<EftTransactionResponse>>> Post(int accountId)
    {
        var operation = new GetMoneyTransferByAccountId(accountId);
        var result = await mediator.Send(operation);
        return result;
    }

}