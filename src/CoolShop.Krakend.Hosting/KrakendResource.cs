namespace CoolShop.Krakend.Hosting;

/// <summary>
///     https://github.com/NapalmCodes/Aspire.Hosting.Krakend/blob/main/NapalmCodes.Aspire.Hosting.Krakend/KrakendResource.cs
/// </summary>
/// <param name="name"></param>
public sealed class KrakendResource(string name) : ContainerResource(name), IResourceWithConnectionString
{
    internal const string PrimaryEndpointName = "http";

    private EndpointReference? _primaryEndpoint;

    public EndpointReference PrimaryEndpoint => _primaryEndpoint ??= new(this, PrimaryEndpointName);

    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create($"{PrimaryEndpoint.Property(EndpointProperty.Url)}");
}
