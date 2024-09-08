var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr(options => options.EnableTelemetry = true);

// Parameters
var adminUsername = builder.AddParameter("keycloak-username", true);
var adminPassword = builder.AddParameter("keycloak-password", true);
var postgresUser = builder.AddParameter("postgres-user", true);
var postgresPassword = builder.AddParameter("postgres-password", true);

// Components
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
var promotionDb = mongodb.AddDatabase(ServiceName.Database.Promotion);
var ratingDb = mongodb.AddDatabase(ServiceName.Database.Rating);

var storage = builder.AddAzureStorage("storage");

var keycloak = builder
    .AddKeycloak(ServiceName.Keycloak, 8000, adminUsername, adminPassword)
    .WithArgs("--features=preview")
    .WithDataBindMount("../../mnt/keycloak");

if (builder.Environment.IsDevelopment())
{
    storage.RunAsEmulator(config => config
        .WithImageTag("3.30.0")
        .WithDataBindMount("../../mnt/azurite"));

    keycloak.WithRealmImport("../../keycloak");
}

var blobs = storage.AddBlobs(ServiceName.Blob);

var pubsub = builder.AddDaprPubSub(ServiceName.Dapr.PubSub,
    new() { LocalPath = Path.Combine(Directory.GetCurrentDirectory(), "../../dapr/components/pubsub.yaml") });

var statestore = builder.AddDaprStateStore(ServiceName.Dapr.StateStore,
    new() { LocalPath = Path.Combine(Directory.GetCurrentDirectory(), "../../dapr/components/statestore.yaml") });

var lockstore = builder.AddDaprComponent(ServiceName.Dapr.LockStore, "lock.redis",
    new() { LocalPath = Path.Combine(Directory.GetCurrentDirectory(), "../../dapr/components/lockstore.yaml") });

var email = builder.AddDaprComponent(ServiceName.Dapr.Smtp, "bindings.smtp",
    new() { LocalPath = Path.Combine(Directory.GetCurrentDirectory(), "../../dapr/components/email.yaml") });

var daprOptions = new DaprSidecarOptions
{
    LogLevel = "debug",
    Config = Path.Combine(Directory.GetCurrentDirectory(), "../../dapr/configuration/config.yaml")
};

builder.AddMailDev("email", 1025);

// Gateway
builder.AddProject<CoolShop_Gateway>(ServiceName.AppId.Gateway)
    .WithDaprSidecar(daprOptions)
    .WithReference(keycloak)
    .WithExternalHttpEndpoints();

// Services
var catalogApi = builder
    .AddProject<CoolShop_Catalog>(ServiceName.AppId.Catalog)
    .WithDaprSidecar(daprOptions)
    .WithReference(pubsub)
    .WithReference(statestore)
    .WithReference(blobs)
    .WithReference(catalogDb)
    .WithReference(keycloak);

var inventoryApi = builder
    .AddProject<CoolShop_Inventory>(ServiceName.AppId.Inventory)
    .WithReference(inventoryDb)
    .WithReference(statestore)
    .WithDaprSidecar(daprOptions);

var cartApi = builder
    .AddProject<CoolShop_Cart>(ServiceName.AppId.Cart)
    .WithDaprSidecar(daprOptions)
    .WithReference(pubsub)
    .WithReference(statestore)
    .WithReference(keycloak);

var orderingApi = builder
    .AddProject<CoolShop_Ordering>(ServiceName.AppId.Ordering)
    .WithDaprSidecar(daprOptions)
    .WithReference(pubsub)
    .WithReference(lockstore)
    .WithReference(statestore)
    .WithReference(orderingDb)
    .WithReference(keycloak);

var ratingApi = builder
    .AddProject<CoolShop_Rating>(ServiceName.AppId.Rating)
    .WithDaprSidecar(daprOptions)
    .WithReference(pubsub)
    .WithReference(statestore)
    .WithReference(ratingDb)
    .WithReference(keycloak);

var notificationApi = builder
    .AddProject<CoolShop_Notification>(ServiceName.AppId.Notification)
    .WithDaprSidecar(daprOptions)
    .WithReference(email)
    .WithReference(pubsub);

var promotionApi = builder
    .AddProject<CoolShop_Promotion>(ServiceName.AppId.Promotion)
    .WithDaprSidecar(daprOptions)
    .WithReference(promotionDb)
    .WithReference(keycloak);

// Dashboard
builder.AddHealthChecksUi("healthchecksui")
    .WithReference(catalogApi)
    .WithReference(inventoryApi)
    .WithReference(cartApi)
    .WithReference(orderingApi)
    .WithReference(ratingApi)
    .WithReference(promotionApi)
    .WithReference(notificationApi)
    .WithExternalHttpEndpoints();

builder.AddExecutable("dapr-dashboard", "dapr", ".", "dashboard")
    .WithHttpEndpoint(8080, 8080, "dapr-dashboard", isProxied: false)
    .ExcludeFromManifest();

builder.Build().Run();
