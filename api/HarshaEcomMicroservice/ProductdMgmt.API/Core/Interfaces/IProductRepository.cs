namespace ProductMgmt.API.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetProductsAsync(GetProductsFilter filter);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
}
