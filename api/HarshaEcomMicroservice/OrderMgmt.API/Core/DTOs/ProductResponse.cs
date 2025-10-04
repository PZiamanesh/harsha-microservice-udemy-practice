namespace OrderMgmt.API.Core.DTOs;

public record ProductResponse
{
    public required Guid ProductID { get; init; }
    public required string ProductName { get; init; }
    public required ProductCategory Category { get; init; }
    public required decimal UnitPrice { get; init; }
    public required int QuantityInStock { get; init; }
}