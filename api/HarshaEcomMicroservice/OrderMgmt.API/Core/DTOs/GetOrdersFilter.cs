namespace OrderMgmt.API.Core.DTOs;

public class GetOrdersFilter : IRequest<ErrorOr<IEnumerable<OrderResponse>>>
{
    public Guid? UserId { get; set; }
    public DateTime? OrderDate { get; set; }
}


public class GetOrdersFilterValidator : AbstractValidator<GetOrdersFilter>
{
    public GetOrdersFilterValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty)
            .When(x => x.UserId.HasValue)
            .WithMessage("User ID cannot be empty");

        RuleFor(x => x.OrderDate)
            .LessThanOrEqualTo(DateTime.Now)
            .When(x => x.OrderDate.HasValue)
            .WithMessage("Order date cannot be in the future");
    }
}