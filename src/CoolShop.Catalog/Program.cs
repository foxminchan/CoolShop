﻿using CoolShop.Catalog.Extensions;
using CoolShop.Catalog.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

app.MapEndpoints();

app.MapDefaultEndpoints();

app.UseOpenApi();

app.UseCloudEvents();

app.MapGrpcService<ProductService>();

app.Run();
