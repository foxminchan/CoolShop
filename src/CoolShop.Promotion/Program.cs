﻿using CoolShop.Promotion.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseExceptionHandler();

app.UseAuthorization();

app.MapEndpoints();

app.MapDefaultEndpoints();

app.UseOpenApi();

app.MapGrpcService<PromotionService>();

app.Run();
