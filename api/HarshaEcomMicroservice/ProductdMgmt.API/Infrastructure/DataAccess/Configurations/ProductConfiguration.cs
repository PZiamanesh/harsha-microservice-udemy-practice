namespace ProductdMgmt.API.Infrastructure.DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductID);
        builder.Property(p => p.ProductName).HasMaxLength(100);
        builder.Property(p => p.Category).HasMaxLength(50);
        builder.Property(p => p.UnitPrice);
        builder.Property(p => p.QuantityInStock);
    }
}
