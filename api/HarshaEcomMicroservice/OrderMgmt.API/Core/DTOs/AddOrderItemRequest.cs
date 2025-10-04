namespace OrderMgmt.API.Core.DTOs;

public record AddOrderItemRequest
{
    public Guid ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}


public class AddOrderItemRequestValidator : AbstractValidator<AddOrderItemRequest>
{
    public AddOrderItemRequestValidator()
    {
        RuleFor(x => x.ProductID)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than 0")
            .PrecisionScale(18,2,false)
            .WithMessage("Unit price cannot have more than 2 decimal places");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be at least 1");
    }
}