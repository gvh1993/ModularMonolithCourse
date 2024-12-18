using Bogus;
using Evently.Modules.Attendance.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Attendance.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new();
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly AttendanceDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<AttendanceDbContext>();
    }

    protected async Task CleanDatabaseAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            """
            DELETE FROM attendance.inbox_message_consumers;
            DELETE FROM attendance.inbox_messages;
            DELETE FROM attendance.outbox_message_consumers;
            DELETE FROM attendance.outbox_messages;
            DELETE FROM attendance.attendees;
            DELETE FROM attendance.events;
            DELETE FROM attendance.tickets;
            DELETE FROM attendance.event_statistics;
            """);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
