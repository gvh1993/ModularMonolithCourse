using Evently.Common.Domain;
using MediatR;

namespace Evently.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
