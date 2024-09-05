using CoolShop.Ordering.Constants;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CoolShop.Ordering.Filters;

internal sealed class IdempotencyFilter(DaprClient daprClient) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.HttpContext.Request;
        var requestMethod = request.Method;
        var requestPath = request.Path;
        var requestId = request.Headers[Http.Idempotency].FirstOrDefault();

        if (requestMethod is not Http.Methods.Post and not Http.Methods.Patch)
        {
            return await next(context);
        }

        List<ValidationFailure> errors = [];

        if (string.IsNullOrEmpty(requestId))
        {
            errors.Add(new(Http.Idempotency,
                $"{Http.Idempotency} header is required for {Http.Methods.Post} and {Http.Methods.Patch} requests."));
            throw new ValidationException(errors.AsEnumerable());
        }

        var key = $"idempotency:{requestMethod}:{requestPath}:{requestId}";

        var exists = await daprClient.GetStateEntryAsync<bool>(ServiceName.Dapr.StateStore, key);

        if (exists.Value)
        {
            errors.Add(new(Http.Idempotency, $"Idempotency key {requestId} has already been used."));
            throw new ValidationException(errors.AsEnumerable());
        }

        Idempotent idempotent = new() { Id = key, Name = request.GetType().Name };

        await daprClient.SaveStateAsync(ServiceName.Dapr.StateStore, key, idempotent,
            metadata: new Dictionary<string, string> { { "ttlInSeconds", "60" } });

        return await next(context);
    }

    internal sealed class Idempotent
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

internal sealed class FromIdempotencyHeader : FromHeaderAttribute
{
    public new string Name => Http.Idempotency;
}
