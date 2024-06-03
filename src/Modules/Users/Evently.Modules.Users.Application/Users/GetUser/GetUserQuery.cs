using Evently.Common.Application.Messaging;

namespace Evently.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
