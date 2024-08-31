﻿using CoolShop.Shared.ActivityScope;
using CoolShop.Shared.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace CoolShop.ServiceDefaults;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.ConfigureCors();

        builder.ConfigureOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static IHostApplicationBuilder ConfigureCors(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.EnableEnrichment();
        builder.Services.AddLogEnricher<ApplicationEnricher>();

        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddMeter(ActivitySourceProvider.DefaultSourceName);
            })
            .WithTracing(tracing =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    tracing.SetSampler(new AlwaysOnSampler());
                }

                tracing.AddAspNetCoreInstrumentation()
                    .AddGrpcClientInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSource(ActivitySourceProvider.DefaultSourceName);
            });

        builder.AddOpenTelemetryExporters();

        return builder;
    }

    private static void AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }
    }

    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        var healthChecksConfiguration = builder.Configuration.GetSection("HealthChecks");

        var healthChecksRequestTimeout =
            healthChecksConfiguration.GetValue<TimeSpan?>("RequestTimeout") ?? TimeSpan.FromSeconds(5);
        builder.Services.AddRequestTimeouts(timeouts => timeouts.AddPolicy("HealthChecks", healthChecksRequestTimeout));

        var healthChecksExpireAfter =
            healthChecksConfiguration.GetValue<TimeSpan?>("ExpireAfter") ?? TimeSpan.FromSeconds(10);
        builder.Services.AddOutputCache(caching =>
            caching.AddPolicy("HealthChecks", policy => policy.Expire(healthChecksExpireAfter)));

        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        app.UseCors("AllowAll");

        var healthChecks = app.MapGroup("");

        healthChecks
            .CacheOutput("HealthChecks")
            .WithRequestTimeout("HealthChecks");

        healthChecks.MapHealthChecks("/health");

        healthChecks.MapHealthChecks("/alive", new() { Predicate = r => r.Tags.Contains("live") });

        var healthChecksUrls = app.Configuration["HEALTHCHECKSUI_URLS"];

        if (string.IsNullOrWhiteSpace(healthChecksUrls))
        {
            return app;
        }

        var pathToHostsMap = GetPathToHostsMap(healthChecksUrls);

        foreach (var path in pathToHostsMap.Keys)
        {
            healthChecks.MapHealthChecks(path, new() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse })
                .RequireHost(pathToHostsMap[path]);
        }

        return app;
    }

    private static Dictionary<string, string[]> GetPathToHostsMap(string healthChecksUrls)
    {
        var uris = healthChecksUrls.Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(url => new Uri(url, UriKind.Absolute))
            .GroupBy(uri => uri.AbsolutePath, uri => uri.Authority)
            .ToDictionary(g => g.Key, g => g.ToArray());

        return uris;
    }
}
