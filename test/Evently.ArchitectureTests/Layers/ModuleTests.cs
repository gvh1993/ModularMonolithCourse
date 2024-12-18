using System.Reflection;
using Evently.ArchitectureTests.Abstractions;
using Evently.Common.Application.EventBus;
using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Infrastructure;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.Infrastructure;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Infrastructure;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure;
using NetArchTest.Rules;

namespace Evently.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UsersModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [EventsNamespace, TicketingNamespace, AttendanceNamespace];
        string[] integrationEventsModules = [
            EventsIntegrationEventsNamespace,
            TicketingIntegrationEventsNamespace,
            AttendanceIntegrationEventsNamespace];

        List<Assembly> usersAssemblies =
        [
            typeof(User).Assembly,
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.Users.Presentation.AssemblyReference.Assembly,
            typeof(UsersModule).Assembly
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void EventsModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, TicketingNamespace, AttendanceNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            TicketingIntegrationEventsNamespace,
            AttendanceIntegrationEventsNamespace];

        List<Assembly> eventsAssemblies =
        [
            typeof(TicketType).Assembly,
            Modules.Events.Application.AssemblyReference.Assembly,
            Modules.Events.Presentation.AssemblyReference.Assembly,
            typeof(EventsModule).Assembly
        ];

        Types.InAssemblies(eventsAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void TicketingModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, EventsNamespace, AttendanceNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            EventsIntegrationEventsNamespace,
            AttendanceIntegrationEventsNamespace];

        List<Assembly> ticketingAssemblies =
        [
            typeof(Order).Assembly,
            Modules.Ticketing.Application.AssemblyReference.Assembly,
            Modules.Ticketing.Presentation.AssemblyReference.Assembly,
            typeof(TicketingModule).Assembly
        ];

        Types.InAssemblies(ticketingAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void AttendanceModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, EventsNamespace, TicketingNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            EventsIntegrationEventsNamespace,
            TicketingIntegrationEventsNamespace];

        List<Assembly> attendanceAssemblies =
        [
            typeof(Attendee).Assembly,
            Modules.Attendance.Application.AssemblyReference.Assembly,
            Modules.Attendance.Presentation.AssemblyReference.Assembly,
            typeof(AttendanceModule).Assembly
        ];

        Types.InAssemblies(attendanceAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
