using System.Reflection;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Infrastructure;

namespace Evently.Modules.Ticketing.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Ticketing.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Order).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(TicketingModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Ticketing.Presentation.AssemblyReference).Assembly;
}
