namespace OrderMgmt.API.Core.DTOs;

public struct GetProductsRequest : IRequest<ErrorOr<List<ProductResponse>>>
{
}
