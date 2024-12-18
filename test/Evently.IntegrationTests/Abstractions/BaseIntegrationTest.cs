using Bogus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.IntegrationTests.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly Faker Faker = new();

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
