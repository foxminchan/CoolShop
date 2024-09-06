using DurableTask.Core.Exceptions;

namespace CoolShop.Ordering.Workflows;

public sealed class CancelOrderWorkflow : Workflow<CancelOrderWorkflowRequest, CancelOrderWorkflowResult>
{
    public override async Task<CancelOrderWorkflowResult> RunAsync(WorkflowContext context,
        CancelOrderWorkflowRequest input)
    {
        var retryOptions = new WorkflowTaskOptions
        {
            RetryPolicy = new(
                firstRetryInterval: TimeSpan.FromMinutes(1),
                backoffCoefficient: 2.0,
                maxRetryInterval: TimeSpan.FromHours(1),
                maxNumberOfAttempts: 10)
        };

        var result = await context.CallActivityAsync<CancelOrderActivityResult>(
            nameof(CancelOrderActivity),
            input.Id,
            retryOptions);

        if (result.IsSuccess)
        {
            try
            {
                context.SetCustomStatus("Waiting for catalog to be updated");

                await context.WaitForExternalEventAsync<CatalogUpdatedIntegrationEvent>(
                    nameof(CatalogUpdatedIntegrationEvent),
                    TimeSpan.FromMinutes(1));

                var message = $"Your order with id {input.Id} has been cancelled because of several reasons";

                await context.CallActivityAsync(
                    nameof(NotifyActivity),
                    new NotifyActivityData(result.BuyerEmail, message),
                    retryOptions);
            }
            catch (TaskCanceledException)
            {
                context.SetCustomStatus("Order cancellation failed due to timeout");
                return new(false);
            }
            catch (Exception ex) when (ex.InnerException is TaskFailedException)
            {
                context.SetCustomStatus("Order cancellation failed due to an exception");
                return new(false);
            }
        }
        else
        {
            context.SetCustomStatus("Order cancellation failed due to an exception");
            return new(false);
        }

        return new(true);
    }
}
