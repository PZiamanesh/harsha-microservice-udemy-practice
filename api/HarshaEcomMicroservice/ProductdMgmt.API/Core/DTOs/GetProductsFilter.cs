namespace ProductMgmt.API.Core.DTOs;

public class GetProductsFilter : IRequest<ErrorOr<List<ProductResponse>>>
{
    public ProductCategory? Category { get; set; }
    public string? ProductName { get; set; }
}
