namespace ProductdMgmt.API.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid Id);

    Task<Product?> GetProductByAsync(Expression<Func<Product,bool>> predicate);

    Task<IEnumerable<Product>> GetProductsAsync();

    Task<IEnumerable<Product>> GetProductsByAsync(Expression<Func<Product, bool>> predicate);

    void AddProduct(Product product);

    void UpdateProduct(Product product);

    void DeleteProduct(Product product);
}
