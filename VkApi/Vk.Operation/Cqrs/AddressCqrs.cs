using MediatR;
using Vk.Base.Response;
using Vk.Schema;

public record CreateAddressCommand(AddressRequest Model) : IRequest<ApiResponse<AddressResponse>>;
public record UpdateAddressCommand(int Id,AddressRequest Model) : IRequest<ApiResponse>;
public record DeleteAddressCommand(int Id) : IRequest<ApiResponse>;
public record GetAllAddressQuery () : IRequest<ApiResponse<List<AddressResponse>>>;
public record GetAddressByIdQuery(int Id) : IRequest<ApiResponse<AddressResponse>>;
public record GetAddressByCustomerIdQuery(int CustomerId) : IRequest<ApiResponse<List<AddressResponse>>>; //gets all adresses of a customer