namespace ProductMgmt.API.Core.Entities;

public class Product
{
    public Guid ProductID { get; private set; }
    public string? ProductName { get; private set; }
    public ProductCategory Category { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int QuantityInStock { get; private set; }

    private Product() { }

    public static Product Create(
        string? productName,
        ProductCategory category,
        decimal unitPrice,
        int quantityInStock)
    {
        return new Product
        {
            ProductID = Guid.NewGuid(),
            ProductName = productName,
            Category = category,
            UnitPrice = unitPrice,
            QuantityInStock = quantityInStock
        };
    }

    public void Update(
        string? productName,
        ProductCategory category,
        decimal unitPrice,
        int quantityInStock)
    {
        ProductName = productName;
        Category = category;
        UnitPrice = unitPrice;
        QuantityInStock = quantityInStock;
    }
}
