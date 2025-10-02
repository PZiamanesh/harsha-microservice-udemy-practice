namespace OrderMgmt.API.Core.Entities;

public class Product
{
    public Guid ProductID { get; private set; }
    public string? ProductName { get; private set; }
    public CategoryOptions Category { get; private set; }
    public double UnitPrice { get; private set; }
    public int QuantityInStock { get; private set; }

    private Product() { }

    public static Product Create(
        string? productName,
        CategoryOptions category,
        double unitPrice,
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
        CategoryOptions category,
        double unitPrice,
        int quantityInStock)
    {
        ProductName = productName;
        Category = category;
        UnitPrice = unitPrice;
        QuantityInStock = quantityInStock;
    }
}
