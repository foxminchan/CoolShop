using System.Collections.Immutable;
using Aspire.Hosting.Dapr;
using CoolShop.Constants;
using CoolShop.HealthCheck.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr(options => options.EnableTelemetry = true);

var adminUsername = builder.AddParameter("keycloak-username", true);
var adminPassword = builder.AddParameter("keycloak-password", true);
var postgresUser = builder.AddParameter("postgres-user", true);
var postgresPassword = builder.AddParameter("postgres-password", true);

var postgres = builder
    .AddPostgres("postgres", postgresUser, postgresPassword, 5432)
    .WithPgAdmin()
    .WithDataBindMount("../../mnt/postgres");

var mongodb = builder
    .AddMongoDB("mongodb", 27017)
    .WithMongoExpress()
    .WithDataBindMount("../../mnt/mongodb");

var catalogDb = postgres.AddDatabase(ServiceName.Database.Catalog);
var inventoryDb = postgres.AddDatabase(ServiceName.Database.Inventory);
var orderingDb = postgres.AddDatabase(ServiceName.Database.Ordering);
var promotionDb = postgres.AddDatabase(ServiceName.Database.Promotion);
var ratingDb = mongodb.AddDatabase(ServiceName.Database.Rating);

var daprOptions = new DaprSidecarOptions
{
    Config = Path.Combine(Directory.GetCurrentDirectory(), "../../dapr/configuration/config.yaml"),
    ResourcesPaths = ImmutableHashSet.Create(Directory.GetCurrentDirectory() + "../../../dapr/components")
};

var keycloak = builder
    .AddKeycloak("keycloak", 8000, adminUsername, adminPassword)
    .WithDataBindMount("../../mnt/keycloak");

var catalogApi = builder.AddProject<CoolShop_Catalog>("catalog-api")
    .WithDaprSidecar(daprOptions)
    .WithReference(catalogDb)
    .WithReference(keycloak);

var inventoryApi = builder.AddProject<CoolShop_Inventory>("inventory-api")
    .WithReference(inventoryDb)
    .WithDaprSidecar(daprOptions);

var cartApi = builder.AddProject<CoolShop_Cart>("cart-api")
    .WithDaprSidecar(daprOptions)
    .WithReference(keycloak);

var orderingApi = builder.AddProject<CoolShop_Ordering>("ordering-api")
    .WithDaprSidecar(daprOptions)
    .WithReference(orderingDb)
    .WithReference(keycloak);

var ratingApi = builder.AddProject<CoolShop_Rating>("rating-api")
    .WithDaprSidecar(daprOptions)
    .WithReference(ratingDb)
    .WithReference(keycloak);

var promotionApi = builder.AddNpmApp("promotion-api", "../CoolShop.Promotion")
    .WithDaprSidecar(daprOptions)
    .WithReference(promotionDb)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.AddHealthChecksUi("healthchecksui")
    .WithReference(catalogApi)
    .WithReference(inventoryApi)
    .WithReference(cartApi)
    .WithReference(orderingApi)
    .WithReference(promotionApi)
    .WithReference(ratingApi)
    .WithExternalHttpEndpoints();

builder.AddExecutable("dapr-dashboard", "dapr", ".", "dashboard")
    .WithHttpEndpoint(8080, 8080, "dapr-dashboard", isProxied: false)
    .ExcludeFromManifest();

builder.Build().Run();
