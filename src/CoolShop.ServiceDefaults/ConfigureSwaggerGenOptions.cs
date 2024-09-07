using CoolShop.Constants;

namespace CoolShop.ServiceDefaults;

public sealed class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider, IConfiguration config)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        options.MapType<DateOnly>(() => new()
        {
            Type = "string", Format = "date", Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
        });
        options.CustomSchemaIds(type => type.ToString());

        ConfigureAuthorization(options);
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var openApi = config.GetRequiredSection(nameof(OpenApi)).Get<OpenApi>();

        ArgumentNullException.ThrowIfNull(openApi);

        var info = new OpenApiInfo
        {
            Title = openApi.Document.Title,
            Version = description.ApiVersion.ToString(),
            Description = BuildDescription(description, openApi.Document.Description),
            Contact = new()
            {
                Name = "Nhan Nguyen",
                Email = "nguyenxuannhan407@gmail.com",
                Url = new("https://github.com/foxminchan")
            },
            License = new() { Name = "MIT", Url = new("https://opensource.org/licenses/MIT") }
        };

        return info;
    }

    private static string BuildDescription(ApiVersionDescription api, string? description)
    {
        if (description is null)
        {
            return string.Empty;
        }

        var text = new StringBuilder(description);

        if (api.IsDeprecated)
        {
            if (text.Length > 0)
            {
                if (text[^1] != '.')
                {
                    text.Append('.');
                }

                text.Append(' ');
            }

            text.Append("This API version has been deprecated.");
        }

        if (api.SunsetPolicy is not { } policy)
        {
            return text.ToString();
        }

        if (policy.Date is { } when)
        {
            if (text.Length > 0)
            {
                text.Append(' ');
            }

            text.Append("The API will be sunset on ")
                .Append(when.Date.ToShortDateString())
                .Append('.');
        }

        if (!policy.HasLinks)
        {
            return text.ToString();
        }

        text.AppendLine();

        var rendered = false;

        foreach (var link in policy.Links.Where(l => l.Type == "text/html"))
        {
            if (!rendered)
            {
                text.Append("<h4>Links</h4><ul>");
                rendered = true;
            }

            text.Append("<li><a href=\"");
            text.Append(link.LinkTarget.OriginalString);
            text.Append("\">");
            text.Append(
                StringSegment.IsNullOrEmpty(link.Title)
                    ? link.LinkTarget.OriginalString
                    : link.Title.ToString());
            text.Append("</a></li>");
        }

        if (rendered)
        {
            text.Append("</ul>");
        }

        return text.ToString();
    }

    private void ConfigureAuthorization(SwaggerGenOptions options)
    {
        var url = config["services:keycloak:http:0"];

        if (string.IsNullOrWhiteSpace(url))
        {
            return;
        }

        options.CustomSchemaIds(id => id.FullName!.Replace("+", "-"));

        options.AddSecurityDefinition(ServiceName.Keycloak,
            new()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new()
                {
                    Implicit = new()
                    {
                        AuthorizationUrl = new($"{url}/realms/{nameof(CoolShop)}/protocol/openid-connect/auth"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "openid" }, { "profile", "profile" }
                        }
                    }
                }
            });

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new()
                {
                    Reference = new() { Id = ServiceName.Keycloak, Type = ReferenceType.SecurityScheme, },
                    In = ParameterLocation.Header,
                    Name = "Bearer",
                    Scheme = "Bearer",
                },
                []
            }
        };

        options.AddSecurityRequirement(securityRequirement);
    }
}
