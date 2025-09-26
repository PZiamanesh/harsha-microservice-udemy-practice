namespace UserMgmt.API.Infrastructure.BehaviourPipeline;

public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        this._validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationErrors = _validators
            .Select(v => v.Validate(request))
            .Where(res => res.IsValid == false)
            .SelectMany(res => res.Errors)
            .ToList();

        if (!validationErrors.Any())
        {
            return await next();
        }

        var errors = validationErrors.ConvertAll(err => Error.Validation(
            code: err.PropertyName,
            description: err.ErrorMessage));

        return (dynamic)errors;
    }
}
