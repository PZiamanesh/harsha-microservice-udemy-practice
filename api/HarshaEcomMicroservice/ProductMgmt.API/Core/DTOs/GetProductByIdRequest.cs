namespace ProductMgmt.API.Core.DTOs;

public struct GetProductByIdRequest : IRequest<ErrorOr<ProductResponse>>
{
    public Guid ProductID { get; set; }
}
