namespace ProductMgmt.API.Infrastructure.Repositories;

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

    public async Task<IEnumerable<Product>> GetProductsAsync(GetProductsFilter filter)
    {
        var query = _dbContext.Products.AsNoTracking().AsQueryable();

        if (filter.Category != null)
        {
            query = query.Where(p => p.Category == filter.Category);
        }
        if (!string.IsNullOrEmpty(filter.ProductName))
        {
            query = query.Where(p => p.ProductName!.Contains(filter.ProductName));
        }

        return await query.ToListAsync();
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
