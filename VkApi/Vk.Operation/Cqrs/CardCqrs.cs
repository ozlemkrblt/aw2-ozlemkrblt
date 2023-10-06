using MediatR;
using Vk.Base.Response;
using Vk.Schema;
namespace Vk.Operation;


public record CreateCardCommand(CardRequest Model) : IRequest<ApiResponse<CardResponse>>; 
public record UpdateCardCommand(int Id , CardRequest Model) : IRequest<ApiResponse>; 
public record DeleteCardCommand(int Id) : IRequest<ApiResponse>;
public record GetAllCardsQuery() : IRequest<ApiResponse<List<CardResponse>>>;
public record GetCardByIdQuery(int Id) : IRequest<ApiResponse<CardResponse>>;
public record GetCardsByAccountIdQuery(int Id) : IRequest<ApiResponse<List<CardResponse>>>;
public record GetCardsByCustomerIdQuery(int Id) : IRequest<ApiResponse<List<CardResponse>>>;
