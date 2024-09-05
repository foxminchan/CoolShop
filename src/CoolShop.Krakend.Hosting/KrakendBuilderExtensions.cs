namespace CoolShop.Krakend.Hosting;

/// <summary>
///     https://github.com/NapalmCodes/Aspire.Hosting.Krakend/blob/main/NapalmCodes.Aspire.Hosting.Krakend/KrakendBuilderExtensions.cs
/// </summary>
public static class KrakendBuilderExtensions
{
    private const string KrakendContainerConfigDirectory = "/etc/krakend";

    public static IResourceBuilder<KrakendResource> AddKrakend(
        this IDistributedApplicationBuilder builder,
        string name,
        int? port = null)
    {
        var krakendResource = new KrakendResource(name);

        var resourceBuilder = builder.AddResource(krakendResource)
            .WithHttpEndpoint(
                port,
                name: KrakendResource.PrimaryEndpointName,
                targetPort: 8080)
            .WithImage(KrakendContainerImageTags.Image, KrakendContainerImageTags.Tag)
            .WithImageRegistry(KrakendContainerImageTags.Registry)
            .WithOtlpExporter();

        return resourceBuilder;
    }

    public static IResourceBuilder<KrakendResource> WithConfigBindMount(
        this IResourceBuilder<KrakendResource> builder,
        string source,
        bool isReadOnly = false)
    {
        return builder.WithBindMount(source, KrakendContainerConfigDirectory, isReadOnly);
    }
}

internal static class KrakendContainerImageTags
{
    public const string Registry = "docker.io";

    public const string Image = "devopsfaith/krakend";

    public const string Tag = "2.7.0";
}
