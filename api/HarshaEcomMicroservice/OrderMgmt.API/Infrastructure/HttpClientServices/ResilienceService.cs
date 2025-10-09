using Polly.Extensions.Http;
using Polly;

namespace OrderMgmt.API.Infrastructure.HttpClientServices;

public class ResilienceService
{
    private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;
    private readonly IAsyncPolicy<HttpResponseMessage> _circuitBreakerPolicy;
    private readonly IAsyncPolicy<HttpResponseMessage> _retryAndCircuitBreakPolicy;

    public ResilienceService()
    {
        _retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                retryCount: 2,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    Console.WriteLine($"Retry {retryAttempt} after {timespan.TotalSeconds}s delay due to: {outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString()}");
                });

        _circuitBreakerPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromSeconds(10),
                onBreak: (outcome, duration) =>
                {
                    Console.WriteLine($"Circuit breaker opened for {duration.TotalSeconds}s due to: {outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString()}");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker reset");
                });

        _retryAndCircuitBreakPolicy = Policy.WrapAsync(_circuitBreakerPolicy, _retryPolicy);
    }

    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return _retryPolicy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return _circuitBreakerPolicy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetRetryThenCircucitBreakPolicy()
    {
        return _retryAndCircuitBreakPolicy;
    }
}
