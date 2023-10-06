using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Schema;
namespace Vk.Operation;

public class EftTransactionQueryHandler :
    IRequestHandler<GetEftTransactionByRefNoQuery, ApiResponse<List<EftTransactionResponse>>>,
    IRequestHandler<GetEftTransactionByAccountIdQuery, ApiResponse<List<EftTransactionResponse>>>
{

    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public EftTransactionQueryHandler(VkDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetEftTransactionByRefNoQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<EftTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.ReferenceNumber == request.ReferenceNumber).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<EftTransactionResponse>>(list);
        return new ApiResponse<List<EftTransactionResponse>>(mapped);
    }

    public async Task<ApiResponse<List<EftTransactionResponse>>> Handle(GetEftTransactionByAccountIdQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<EftTransaction>().Include(x => x.Account).ThenInclude(x => x.Customer)
            .Where(x => x.AccountId == request.AccountId).ToListAsync(cancellationToken);

        var mapped = mapper.Map<List<EftTransactionResponse>>(list);
        return new ApiResponse<List<EftTransactionResponse>>(mapped);
    }
}