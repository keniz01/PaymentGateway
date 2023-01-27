using FluentAssertions;
using NetArchTest.Rules;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Domain;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.Application;

[TestFixture]
public class ApplicationDependencyTests
{
    [Test]
    public void Ensure_Application_layer_depends_on_domain_layer()
    {
        var result = Types.InAssembly(ApplicationAssembly.GetAssemblyReference())
            .That()
            .ResideInNamespace(ApplicationAssembly.GetAssemblyReference().FullName)
            .Should()
            .HaveDependencyOn(DomainAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}