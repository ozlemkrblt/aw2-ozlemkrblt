using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.CustomRepository;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Command;



public class CardCommandHandler :     IRequestHandler<CreateCardCommand, ApiResponse<CardResponse>>,
    IRequestHandler<UpdateCardCommand, ApiResponse>,
    IRequestHandler<DeleteCardCommand, ApiResponse>
{
private readonly VkDbContext dbContext;
private readonly IMapper mapper;

public CardCommandHandler(VkDbContext dbContext , IMapper mapper)
{
    this.dbContext = dbContext;
    this.mapper = mapper;
}

public async Task<ApiResponse<CardResponse>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
{

    Card mapped = mapper.Map<Card>(request.Model);

    var entity = await dbContext.Set<Card>().AddAsync(mapped, cancellationToken);
    await dbContext.SaveChangesAsync(cancellationToken);

    var response = mapper.Map<CardResponse>(entity.Entity);
    return new ApiResponse<CardResponse>(response);


}

public async Task<ApiResponse> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
{

    var entity = await dbContext.Set<Card>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    if (entity == null)
    {
        return new ApiResponse("Record not found!");
    }
        
            entity.CardHolder = request.Model.CardHolder; //Cardholder name can be different than the customer name.
            //Card number is valid until it is expired,it cannot be changed with update.
            entity.ExpiryDate = request.Model.ExpiryDate; //Expiry date can be changed if new card number is the same with the previous one.
            entity.Cvv = request.Model.Cvv; // Cvv is changed if new card number is the same with previous one.
            entity.ExpenseLimit = request.Model.ExpenseLimit;   
            entity.UpdateDate = DateTime.UtcNow;
    await dbContext.SaveChangesAsync(cancellationToken);

    return new ApiResponse();


}


public async Task<ApiResponse> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
{


    var entity = await dbContext.Set<Card>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
    if (entity == null)
    {
        return new ApiResponse("Record not found!");
    }
    entity.IsActive = false;
    await dbContext.SaveChangesAsync(cancellationToken);

    return new ApiResponse();




}




}