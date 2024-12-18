using System.Reflection;
using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Infrastructure;

namespace Evently.Modules.Attendance.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = typeof(Attendance.Application.AssemblyReference).Assembly;

    protected static readonly Assembly DomainAssembly = typeof(Attendee).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(AttendanceModule).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(Attendance.Presentation.AssemblyReference).Assembly;
}
