using System.Reflection;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure;

namespace Evently.Modules.Events.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Events.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Event).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(EventsModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Events.Presentation.AssemblyReference).Assembly;
}
