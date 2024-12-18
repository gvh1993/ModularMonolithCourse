using Evently.Common.Application.Exceptions;
using Evently.Common.Infrastructure.Authentication;
using Evently.Modules.Ticketing.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Evently.Modules.Ticketing.Infrastructure.Authentication;

internal sealed class CustomerContext(IHttpContextAccessor httpContextAccessor) : ICustomerContext
{
    public Guid CustomerId => httpContextAccessor.HttpContext?.User.GetUserId() ??
                              throw new EventlyException("User identifier is unavailable");
}
