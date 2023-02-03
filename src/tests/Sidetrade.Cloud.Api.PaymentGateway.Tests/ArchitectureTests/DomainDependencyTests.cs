using FluentAssertions;
using NetArchTest.Rules;
namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.Application;

[TestFixture]
public class DomainDependencyTests
{
    [Test]
    public void Assert_Domain_layer_does_not_depend_on_any_layer()
    {
        var result = Types.InAssembly(DomainAssembly.GetAssemblyReference())
            .That()
            .ResideInNamespace(DomainAssembly.GetAssemblyReference().FullName)
            .ShouldNot()
            .HaveDependencyOnAll(
                ApplicationAssembly.GetAssemblyReference().FullName,
                PresentationAssembly.GetAssemblyReference().FullName,
                ApiAssembly.GetAssemblyReference().FullName,
                InfrastrucatureAssembly.GetAssemblyReference().FullName,
                DomainAssembly.GetAssemblyReference().FullName
            )
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}