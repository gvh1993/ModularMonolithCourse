using Evently.Common.Domain;
using Evently.Common.Presentation.Endpoints;
using Evently.Common.Presentation.Results;
using Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;

internal sealed class CreateTicketType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("ticket-types", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateTicketTypeCommand(
                request.EventId,
                request.Name,
                request.Price,
                request.Currency,
                request.Quantity));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.TicketTypes);
    }

    internal sealed class Request
    {
        public Guid EventId { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public string Currency { get; init; }

        public decimal Quantity { get; init; }
    }
}
