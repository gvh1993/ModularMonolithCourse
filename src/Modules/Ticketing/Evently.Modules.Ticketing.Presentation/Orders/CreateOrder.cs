using Evently.Common.Domain;
using Evently.Common.Presentation.Endpoints;
using Evently.Common.Presentation.Results;
using Evently.Modules.Ticketing.Application.Abstractions.Authentication;
using Evently.Modules.Ticketing.Application.Orders.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation.Orders;

internal sealed class CreateOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (ICustomerContext customerContext, ISender sender) =>
        {
            Result result = await sender.Send(new CreateOrderCommand(customerContext.CustomerId));

            return result.Match(() => Results.Ok(), ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateOrder)
        .WithTags(Tags.Orders);
    }
}
