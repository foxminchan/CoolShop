using Aspire.Hosting.Lifecycle;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoolShop.Ollama.Hosting;

/// <summary>
/// https://github.com/dotnet/eShopSupport/blob/main/src/AppHost/Ollama/OllamaResourceExtensions.cs
/// </summary>
public static class OllamaResourceExtensions
{
    public static IResourceBuilder<OllamaResource> AddOllama(this IDistributedApplicationBuilder builder, string name,
        string[]? models = null, string? defaultModel = null, bool enableGpu = true, int? port = null)
    {
        defaultModel ??= "mistral:v0.3";

        if (models is null or { Length: 0 })
        {
            models = [defaultModel];
        }

        var resource = new OllamaResource(name, models, defaultModel, enableGpu);
        var ollama = builder.AddResource(resource)
            .WithHttpEndpoint(port: port, targetPort: 11434)
            .WithImage("ollama/ollama");

        if (enableGpu)
        {
            ollama = ollama.WithContainerRuntimeArgs("--gpus=all");
        }

        builder.Services.TryAddLifecycleHook<OllamaEnsureModelAvailableHook>();

        builder.AddResource(new OllamaModelDownloaderResource($"ollama-model-downloader-{name}", resource))
            .WithInitialState(new()
            {
                Properties = [], ResourceType = "ollama downloader", State = KnownResourceStates.Hidden
            })
            .ExcludeFromManifest();

        return ollama;
    }

    public static IResourceBuilder<OllamaResource> WithDataVolume(this IResourceBuilder<OllamaResource> builder)
    {
        return builder.WithVolume(CreateVolumeName(builder, builder.Resource.Name), "/root/.ollama");
    }

    public static IResourceBuilder<TDestination> WithReference<TDestination>(
        this IResourceBuilder<TDestination> builder, IResourceBuilder<OllamaResource> ollamaBuilder)
        where TDestination : IResourceWithEnvironment
    {
        return builder
            .WithReference(ollamaBuilder.GetEndpoint("http"))
            .WithEnvironment($"{ollamaBuilder.Resource.Name}:Type", "ollama")
            .WithEnvironment($"{ollamaBuilder.Resource.Name}:LlmModelName", ollamaBuilder.Resource.DefaultModel);
    }

    private static string CreateVolumeName<T>(IResourceBuilder<T> builder, string suffix) where T : IResource
    {
        return (string)typeof(ContainerResource).Assembly
            .GetType("Aspire.Hosting.Utils.VolumeNameGenerator", true)!
            .GetMethod("CreateVolumeName")!
            .MakeGenericMethod(typeof(T))
            .Invoke(null, [builder, suffix])!;
    }

    private sealed class OllamaEnsureModelAvailableHook(
        ResourceLoggerService loggerService,
        ResourceNotificationService notificationService,
        DistributedApplicationExecutionContext context) : IDistributedApplicationLifecycleHook
    {
        public Task AfterEndpointsAllocatedAsync(DistributedApplicationModel appModel,
            CancellationToken cancellationToken = default)
        {
            if (context.IsPublishMode)
            {
                return Task.CompletedTask;
            }

            var client = new HttpClient();

            foreach (var downloader in appModel.Resources.OfType<OllamaModelDownloaderResource>())
            {
                var ollama = downloader.OllamaResource;

                var logger = loggerService.GetLogger(downloader);

                _ = Task.Run(async () =>
                    {
                        var httpEndpoint = ollama.GetEndpoint("http");

                        var ollamaModelsAvailable = await client.GetFromJsonAsync<OllamaGetTagsResponse>(
                            $"{httpEndpoint.Url}/api/tags", new JsonSerializerOptions(JsonSerializerDefaults.Web),
                            cancellationToken: cancellationToken);

                        if (ollamaModelsAvailable is null)
                        {
                            return;
                        }

                        var availableModelNames = ollamaModelsAvailable.Models?.Select(m => m.Name) ?? [];

                        var modelsToDownload = ollama.Models.Except(availableModelNames);

                        var values = modelsToDownload as string[] ?? modelsToDownload.ToArray();

                        if (!values.Any())
                        {
                            return;
                        }

                        logger.LogInformation("Downloading models {Models} for ollama {OllamaName}...",
                            string.Join(", ", values), ollama.Name);

                        await notificationService.PublishUpdateAsync(downloader,
                            s => s with { State = new("Downloading models...", KnownResourceStateStyles.Info) });

                        await Parallel.ForEachAsync(values, cancellationToken, async (modelName, ct) =>
                        {
                            await DownloadModelAsync(logger, httpEndpoint, modelName, ct);
                        });

                        await notificationService.PublishUpdateAsync(downloader,
                            s => s with { State = new("Models downloaded", KnownResourceStateStyles.Success) });
                    },
                    cancellationToken);
            }

            return Task.CompletedTask;
        }

        private static async Task DownloadModelAsync(ILogger logger, EndpointReference httpEndpoint, string? modelName,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Pulling ollama model {ModelName}...", modelName);

            var httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };
            var request =
                new HttpRequestMessage(HttpMethod.Post, $"{httpEndpoint.Url}/api/pull")
                {
                    Content = JsonContent.Create(new { name = modelName })
                };
            var response =
                await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var responseContentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var streamReader = new StreamReader(responseContentStream);
            while (await streamReader.ReadLineAsync(cancellationToken) is { } line)
            {
                logger.Log(LogLevel.Information, 0, line, null, (s, _) => s);
            }

            logger.LogInformation("Finished pulling ollama mode {ModelName}", modelName);
        }

        record OllamaGetTagsResponse(OllamaGetTagsResponseModel[]? Models);

        record OllamaGetTagsResponseModel(string Name);
    }

    private class OllamaModelDownloaderResource(string name, OllamaResource ollamaResource) : Resource(name)
    {
        public OllamaResource OllamaResource { get; } = ollamaResource;
    }
}
