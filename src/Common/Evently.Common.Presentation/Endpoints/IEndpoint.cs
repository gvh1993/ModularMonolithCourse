using Microsoft.AspNetCore.Routing;

namespace Evently.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
