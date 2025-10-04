namespace ProductMgmt.API.Core.DTOs;

public record AddProductRequest : IRequest<ProductResponse>
{
    public string? ProductName { get; set; }
    public ProductCategory Category { get; set; }
    public double UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}


public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
{
    public AddProductRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid category selected.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero.");

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock cannot be negative.");
    }
}