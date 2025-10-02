namespace ProductMgmt.API.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid Id);

    Task<IEnumerable<Product>> GetProductsAsync(GetProductsFilterRequest request);

    void AddProduct(Product product);

    void UpdateProduct(Product product);

    void DeleteProduct(Product product);
}
