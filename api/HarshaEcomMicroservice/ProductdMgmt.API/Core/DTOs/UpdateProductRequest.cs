namespace ProductdMgmt.API.Core.DTOs;

public record UpdateProductRequest : IRequest<ErrorOr<Updated>>
{
    public Guid ProductID { get; set; }
    public string ProductName { get; set; }
    public CategoryOptions Category { get; set; }
    public double UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}


public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.ProductID)
            .NotEmpty().WithMessage("Product ID is required.");

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