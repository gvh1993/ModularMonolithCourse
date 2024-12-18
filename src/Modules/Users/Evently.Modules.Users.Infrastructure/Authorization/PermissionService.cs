using Evently.Common.Application.Authorization;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUserPermissions;
using MediatR;

namespace Evently.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
