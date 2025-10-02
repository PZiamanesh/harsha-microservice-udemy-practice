namespace ProductMgmt.API.Core.DTOs;

public class GetProductsFilterRequest : IRequest<ErrorOr<List<ProductResponse>>>
{
    public CategoryOptions? Category { get; set; }
    public string? ProductName { get; set; }
}
