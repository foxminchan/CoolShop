using System.Diagnostics.CodeAnalysis;
using CoolShop.Inventory.Features.Inventories.Get;
using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using InventoryModel = CoolShop.Inventory.Domain.InventoryAggregator.Inventory;

namespace CoolShop.Inventory.Grpc;

public sealed class InventoryService(ISender sender, ILogger<InventoryService> logger)
    : AppCallback.AppCallbackBase
{
    public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
    {
        if (logger.IsEnabled(LogLevel.Debug))
        {
            logger.LogDebug("[{Service}] - Invoking method: {Method}", nameof(InventoryService), request.Method);
        }

        var response = new InvokeResponse();

        switch (request.Method)
        {
            case nameof(Inventory.InventoryBase.GetInventory):
            {
                var input = request.Data.Unpack<InventoryRequest>();

                var inventory = await sender.Send(new GetInventoryQuery(Guid.Parse(input.InventoryId)));

                if (inventory.Value is null)
                {
                    ThrowNotFound();
                }

                response.Data = Any.Pack(MapToInventoryResponse(inventory.Value));

                return response;
            }
            default:
                logger.LogWarning("[{Service}] - Method not found: {Method}", nameof(InventoryService), request.Method);
                break;
        }

        return response;
    }

    public override Task<ListTopicSubscriptionsResponse> ListTopicSubscriptions(Empty request,
        ServerCallContext context)
    {
        return Task.FromResult(new ListTopicSubscriptionsResponse());
    }

    [DoesNotReturn]
    private static void ThrowNotFound()
    {
        throw new RpcException(new(StatusCode.NotFound, "Inventory not found"));
    }

    private static InventoryResponse MapToInventoryResponse(InventoryModel inventory)
    {
        return new()
        {
            InventoryId = inventory.Id.ToString(),
            QuantityAvailable = inventory.QuantityAvailable,
            QuantityOnHold = inventory.QuantityOnHold
        };
    }
}
