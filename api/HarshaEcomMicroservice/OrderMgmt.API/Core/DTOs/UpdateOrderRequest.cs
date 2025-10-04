namespace OrderMgmt.API.Core.DTOs;

public record UpdateOrderRequest
{
    public Guid OrderID { get; set; }
    public Guid UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public List<UpdateOrderItemRequest> OrderItems { get; set; } = [];
}


public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.OrderID)
            .NotEmpty()
            .WithMessage("Order ID is required");

        RuleFor(x => x.UserID)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.OrderDate)
            .NotEmpty()
            .WithMessage("Order date is required")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Order date cannot be in the future");

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("Order must contain at least one item");

        RuleForEach(x => x.OrderItems)
            .SetValidator(new UpdateOrderItemRequestValidator());
    }
}