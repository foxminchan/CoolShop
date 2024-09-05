namespace CoolShop.Ollama.Hosting;

/// <summary>
///     https://github.com/dotnet/eShopSupport/blob/main/src/AppHost/Ollama/OllamaResource.cs
/// </summary>
/// <param name="name"></param>
/// <param name="models"></param>
/// <param name="defaultModel"></param>
/// <param name="enableGpu"></param>
public sealed class OllamaResource(string name, string[] models, string defaultModel, bool enableGpu)
    : ContainerResource(name)
{
    public string[] Models { get; } = models;
    public string DefaultModel { get; } = defaultModel;
    public bool EnableGpu { get; } = enableGpu;
}
