using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.CustomRepository;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Command;



public class CardQueryHandler :     IRequestHandler<GetAllCardsQuery,  ApiResponse<List<CardResponse>> >,
    IRequestHandler<GetCardByIdQuery, ApiResponse<CardResponse> >,
    IRequestHandler<GetCardsByAccountIdQuery, ApiResponse<List<CardResponse>> >,
IRequestHandler<GetCardsByCustomerIdQuery, ApiResponse<List<CardResponse>> >
{

private readonly VkDbContext dbContext;
private readonly IMapper mapper;

public CardQueryHandler(VkDbContext dbContext, IMapper mapper)
{
    this.dbContext = dbContext;
    this.mapper = mapper;
}

public async Task<ApiResponse<List<CardResponse>>> Handle(GetAllCardsQuery request,
       CancellationToken cancellationToken)
{
    List<Card> list = await dbContext.Set<Card>().Include(x => x.Account).ToListAsync(cancellationToken); //?

    List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);
    return new ApiResponse<List<CardResponse>>(mapped);
}

public async Task<ApiResponse<CardResponse>> Handle(GetCardByIdQuery request,
    CancellationToken cancellationToken)
{
    Card? entity = await dbContext.Set<Card>().Include(x => x.Account)
        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

    if (entity is null)
    {
        return new ApiResponse<CardResponse>("Record not found");
    }

    CardResponse mapped = mapper.Map<CardResponse>(entity);
    return new ApiResponse<CardResponse>(mapped);
}

public async Task<ApiResponse<List<CardResponse>>> Handle(GetCardsByAccountIdQuery request,
    CancellationToken cancellationToken)
{
    List<Card> list = await dbContext.Set<Card>().Include(x => x.Account).Where(x => x.AccountId == request.Id).ToListAsync(cancellationToken);

    List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);
    return new ApiResponse<List<CardResponse>>(mapped);
}

public async Task<ApiResponse<List<CardResponse>>> Handle(GetCardsByCustomerIdQuery request,
    CancellationToken cancellationToken)
{
    List<Card> list = await dbContext.Set<Card>().Include(x => x.Account).Where(x => x.Account.CustomerId == request.Id).ToListAsync(cancellationToken);

    List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);
    return new ApiResponse<List<CardResponse>>(mapped);
}


}