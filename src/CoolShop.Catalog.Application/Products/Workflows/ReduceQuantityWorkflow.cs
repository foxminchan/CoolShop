using CoolShop.Catalog.Application.Products.Activities;
using CoolShop.Catalog.Application.Products.ReduceQuantity;
using CoolShop.Catalog.Domain.IntegrationEvents;
using DurableTask.Core.Exceptions;

namespace CoolShop.Catalog.Application.Products.Workflows;

public sealed class ReduceQuantityWorkflow : Workflow<ReduceQuantityCommand, ReduceQuantityWorkflowResult>
{
    public override async Task<ReduceQuantityWorkflowResult> RunAsync(WorkflowContext context,
        ReduceQuantityCommand input)
    {
        var retryOptions = new WorkflowTaskOptions
        {
            RetryPolicy = new(
                firstRetryInterval: TimeSpan.FromMinutes(1),
                backoffCoefficient: 2.0,
                maxRetryInterval: TimeSpan.FromHours(1),
                maxNumberOfAttempts: 10)
        };

        var inventories = await context.CallActivityAsync<Dictionary<Guid, int>>(
            nameof(RetrieveInventoryActivity),
            new Dictionary<Guid, int>(input.ProductQuantities),
            retryOptions);

        await context.CallActivityAsync(
            nameof(UpdateInventoryActivity),
            new Dictionary<Guid, int>(inventories));

        try
        {
            context.SetCustomStatus("Waiting for inventory to be updated");

            var inventoryUpdated = await context.WaitForExternalEventAsync<InventoryUpdatedIntegrationEvent>(
                nameof(InventoryUpdatedIntegrationEvent),
                TimeSpan.FromMinutes(1));

            await context.CallActivityAsync(
                nameof(SetOutStockProductActivity),
                new Dictionary<Guid, bool>(inventoryUpdated.IsOutOfStock));
        }
        catch (TaskCanceledException)
        {
            context.SetCustomStatus("Inventory update failed due to timeout");
            return new(false);
        }
        catch (Exception ex) when (ex.InnerException is TaskFailedException)
        {
            context.SetCustomStatus("Inventory update failed due to exception");
            return new(false);
        }

        return new(true);
    }
}
