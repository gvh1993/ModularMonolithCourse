using Bogus;
using Evently.Modules.Ticketing.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Ticketing.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new();
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly TicketingDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<TicketingDbContext>();
    }

    protected async Task CleanDatabaseAsync()
    {
        await DbContext.Database.ExecuteSqlRawAsync(
            """
            DELETE FROM ticketing.inbox_message_consumers;
            DELETE FROM ticketing.inbox_messages;
            DELETE FROM ticketing.outbox_message_consumers;
            DELETE FROM ticketing.outbox_messages;
            DELETE FROM ticketing.events;
            DELETE FROM ticketing.ticket_types;
            DELETE FROM ticketing.customers;
            DELETE FROM ticketing.orders;
            DELETE FROM ticketing.order_items;
            DELETE FROM ticketing.tickets;
            DELETE FROM ticketing.payments;
            """);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
