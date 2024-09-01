using CoolShop.Catalog.Extensions;
using CoolShop.Catalog.Grpc;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

app.MapEndpoints();

app.MapDefaultEndpoints();

app.UseOpenApi();

app.UseCloudEvents();

if (!Directory.Exists(Path.Combine(app.Environment.ContentRootPath, "Pics")))
{
    Directory.CreateDirectory(Path.Combine(app.Environment.ContentRootPath, "Pics"));
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Pics")),
    RequestPath = "/Pics"
});

app.MapGrpcService<ProductService>();

app.Run();
