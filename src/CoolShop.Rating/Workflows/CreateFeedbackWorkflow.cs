using DurableTask.Core.Exceptions;

namespace CoolShop.Rating.Workflows;

public sealed class CreateFeedbackWorkflow : Workflow<Feedback, CreateFeedbackWorkflowResult>
{
    public override async Task<CreateFeedbackWorkflowResult> RunAsync(WorkflowContext context, Feedback input)
    {
        var retryOptions = new WorkflowTaskOptions
        {
            RetryPolicy = new(
                firstRetryInterval: TimeSpan.FromMinutes(1),
                backoffCoefficient: 2.0,
                maxRetryInterval: TimeSpan.FromHours(1),
                maxNumberOfAttempts: 10)
        };

        var feedback = await context.CallActivityAsync<Feedback>(
            nameof(CreateFeedbackActivity),
            input,
            retryOptions);

        try
        {
            context.SetCustomStatus("Waiting for catalog to be updated");

            await context.WaitForExternalEventAsync<CatalogUpdatedIntegrationEvent>(
                nameof(CatalogUpdatedIntegrationEvent),
                TimeSpan.FromMinutes(1));
        }
        catch (TaskCanceledException)
        {
            await context.CallActivityAsync(
                nameof(RollbackFeedbackActivity),
                new RollbackFeedbackActivityData(feedback, nameof(CatalogAddedRatingIntegrationEvent)));

            context.SetCustomStatus("Feedback creation failed due to timeout");

            return new(false);
        }
        catch (Exception ex) when (ex.InnerException is TaskFailedException)
        {
            await context.CallActivityAsync(
                nameof(RollbackFeedbackActivity),
                new RollbackFeedbackActivityData(feedback, nameof(CatalogAddedRatingIntegrationEvent)));

            context.SetCustomStatus("Feedback creation failed due to an exception");

            return new(false);
        }

        return new(true);
    }
}
