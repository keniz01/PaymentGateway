using NetArchTest.Rules;
using Sidetrade.Cloud.Api.PaymentGateway.Application.Abstractions.Commands;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Domain;
using MassTransit;
using FluentAssertions;
using Sidetrade.Cloud.Api.PaymentGateway.Contracts;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Sidetrade.Cloud.Api.PaymentGateway.Consumers;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.Application;

[TestFixture]
public class ConsumersDependencyTests
{
    [Test(Description = "Consumers Project should only reference Application, Contracts, " +
        "Domain and Persitence projects.")]
    [Category("ApplicationDependencyTests")]
    public void Consumers_project_depends_on_domain_applications_and_contracts_projects()
    {
        var result = Types
            .InAssembly(ConsumersAssembly.GetAssemblyReference())
            .That()
            .ImplementInterface(typeof(IConsumer<>))
            .Should()
            .HaveDependencyOnAll
            (
                ApplicationAssembly.GetAssemblyReference().FullName,
                ContractsAssembly.GetAssemblyReference().FullName,
                DomainAssembly.GetAssemblyReference().FullName
            )
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
