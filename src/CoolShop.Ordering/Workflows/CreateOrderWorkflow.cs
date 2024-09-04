using CoolShop.Cart.Grpc;

namespace CoolShop.Ordering.Workflows;

public sealed class CreateOrderWorkflow : Workflow<CreateOrderWorkflowRequest, CreateOrderWorkflowResult>
{
    public override async Task<CreateOrderWorkflowResult> RunAsync(WorkflowContext context,
        CreateOrderWorkflowRequest input)
    {
        var retryOptions = new WorkflowTaskOptions
        {
            RetryPolicy = new(
                firstRetryInterval: TimeSpan.FromMinutes(1),
                backoffCoefficient: 2.0,
                maxRetryInterval: TimeSpan.FromHours(1),
                maxNumberOfAttempts: 10),
        };

        var cart = await context.CallActivityAsync<BasketResponse?>(
            nameof(RetrieveBasketActivity),
            input.BuyerId,
            retryOptions);

        Guard.Against.NotFound(input.BuyerId, cart);

        List<Item> items = cart.Products
            .Select(x => new Item(Guid.Parse(x.Id), x.Quantity, (decimal)x.Price))
            .ToList();

        var order = await context.CallActivityAsync<AddOrderActivityResult>(
            nameof(AddOrderActivity),
            new AddOrderActivityData(input.BuyerId, input.Note, input.PaymentMethod, items),
            retryOptions);

        if (order.IsSuccess)
        {
            try
            {
                context.SetCustomStatus("Waiting for catalog and basket to be updated");

                var catalogUpdated = context.WaitForExternalEventAsync<CatalogUpdatedIntegrationEvent>(
                    nameof(CatalogUpdatedIntegrationEvent),
                    TimeSpan.FromMinutes(1));

                var basketUpdated = context.WaitForExternalEventAsync<BasketUpdatedIntegrationEvent>(
                    nameof(BasketUpdatedIntegrationEvent),
                    TimeSpan.FromMinutes(1));

                await Task.WhenAll(catalogUpdated, basketUpdated);

                var message = $"Your order with id {order.OrderId} has been placed successfully";

                await context.CallActivityAsync(
                    nameof(NotifyActivity),
                    new NotifyActivityData(input.Email, message),
                    retryOptions);
            }
            catch (TaskCanceledException)
            {
                return await HandleOrderCancellationAsync(context, order.OrderId, input.Email,
                    "Catalog and basket update failed due to timeout", retryOptions);
            }
            catch (Exception ex) when (ex.InnerException is DurableTask.Core.Exceptions.TaskFailedException)
            {
                return await HandleOrderCancellationAsync(context, order.OrderId, input.Email,
                    "Catalog and basket update failed due to exception", retryOptions);
            }
        }
        else
        {
            return await HandleOrderCancellationAsync(context, order.OrderId, input.Email, "Order creation failed",
                retryOptions);
        }

        return new(order.OrderId, true);
    }

    private static async Task<CreateOrderWorkflowResult> HandleOrderCancellationAsync(
        WorkflowContext context, Guid orderId, string? email, string status, WorkflowTaskOptions retryOptions)
    {
        await context.CallActivityAsync(
            nameof(CancelOrderActivity),
            orderId,
            retryOptions);

        var message = $"Your order with id {orderId} has been cancelled for several reasons. You will be refunded soon";

        await context.CallActivityAsync(
            nameof(NotifyActivity),
            new NotifyActivityData(email, message),
            retryOptions);

        context.SetCustomStatus(status);

        return new(orderId, false);
    }
}
