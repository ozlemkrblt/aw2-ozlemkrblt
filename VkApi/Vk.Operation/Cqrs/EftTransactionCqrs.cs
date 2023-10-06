using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation;

public record CreateEftTransaction(EftTransactionRequest Model) : IRequest<ApiResponse<EftTransactionResponse>>;
public record GetEftTransactionByRefNoQuery(string ReferenceNumber) : IRequest<ApiResponse<List<EftTransactionResponse>>>;
public record GetEftTransactionByAccountIdQuery(int AccountId) : IRequest<ApiResponse<List<EftTransactionResponse>>>;
