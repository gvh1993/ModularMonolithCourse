using FluentAssertions;
using NetArchTest.Rules;

namespace Evently.Modules.Ticketing.ArchitectureTests.Abstractions;

internal static class TestResultExtensions
{
    internal static void ShouldBeSuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.Should().BeEmpty();
    }
}
