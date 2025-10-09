namespace OrderMgmt.API.Core.DTOs;

public record UpdateProductMessage
{
    public Guid ProductID { get; set; }
    public string ProductName { get; set; }
    public ProductCategory Category { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}


public class UpdateProductMessageToProductResponseMapper : Profile
{
    public UpdateProductMessageToProductResponseMapper()
    {
        CreateMap<UpdateProductMessage, ProductResponse>();
    }
}
