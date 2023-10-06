//TODO :authorization and authentication
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Base.Transaction;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Command;

public class EftTransactionCommandHandler :
    IRequestHandler<CreateEftTransaction, ApiResponse<EftTransactionResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public EftTransactionCommandHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<EftTransaction>> Handle(CreateEftTransaction request,
        CancellationToken cancellationToken)
    {

        string refNumber = Guid.NewGuid().ToString().Replace("-", "").ToLower();

        var checkAccount = await CheckAccount(request.Model.AccountId, cancellationToken);
        

        var amount = request.Model.Amount;

        if (amount > 2000)
        {
            request.Model.ChargeAmount = 5;
        }
        else if (amount < 1000)
        {
            request.Model.ChargeAmount = 3;
        }



        var newBalance = await BalanceOperation(request.Model.AccountId, request.Model.Amount,
            request.Model.ChargeAmount, cancellationToken);

        string txnCode = "Eft";

        AccountTransaction eftTransaction = new AccountTransaction();
        eftTransaction.TransactionDate = DateTime.UtcNow;
        eftTransaction.AccountId = request.Model.AccountId;
        eftTransaction.TransactionCode = txnCode;
        eftTransaction.IsActive = true;
        eftTransaction.Description = request.Model.Description;
        eftTransaction.CreditAmount = request.Model.Amount;
        eftTransaction.ReferenceNumber = refNumber;


        await dbContext.Set<AccountTransaction>().AddAsync(eftTransaction, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);


        var response = mapper.Map<EftTransactionResponse>(request.Model);
        response.ReferenceNumber = refNumber;
        response.TransactionCode = txnCode;
        response.TransactionDate = DateTime.UtcNow;
        response.AccountId = request.Model.AccountId;
        response.ReceiverName = request.Model.ReceiverName;

        return new ApiResponse<EftTransactionResponse>(response);
    }

    private async Task<ApiResponse<Account>> CheckAccount(int accountId, CancellationToken cancellationToken)
    {
        var account = await dbContext.Set<Account>().Where(x => x.Id == accountId).FirstOrDefaultAsync(cancellationToken);

        if (account == null)
        {
            return new ApiResponse<Account>("Invalid Account");
        }

        if (!account.IsActive)
        {
            return new ApiResponse<Account>("Invalid Account");
        }

        return new ApiResponse<Account>(account);
    }

    private async Task<ApiResponse> BalanceOperation(int accountId, decimal amount, decimal chargeAmount, CancellationToken cancellationToken)
    {
        var account = await dbContext.Set<Account>().Where(x => x.Id == accountId).FirstOrDefaultAsync(cancellationToken);



        if (account.Balance < amount + chargeAmount)
        {
            return new ApiResponse("Insufficent balance");
        }
        else
        {
            account.Balance -= amount + chargeAmount;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

}