namespace OrderMgmt.API.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MySqlDbContext _dbContext;

    public ProductRepository(MySqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetProductByIdAsync(Guid Id)
    {
        return await _dbContext.Products.SingleOrDefaultAsync(p => p.ProductID == Id);
    }

    public async Task<Product?> GetProductByAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbContext.Products.Where(predicate).ToListAsync();
    }

    public void AddProduct(Product product)
    {
        _dbContext.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        _dbContext.Update(product);
    }

    public void DeleteProduct(Product product)
    {
        _dbContext.Remove(product);
    }
}
