using FluentAssertions;
using NetArchTest.Rules;
using Sidetrade.Cloud.Api.PaymentGateway.Api;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Consumers;
using Sidetrade.Cloud.Api.PaymentGateway.Contracts;
using Sidetrade.Cloud.Api.PaymentGateway.Domain;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.Application;

[TestFixture]
public class ContractsDependencyTests
{
    [Test(Description = "Contracts Project is not depend on any other project.")]
    [Category("ContractsDependencyTests")]
    public void Contracts_project_should_not_depend_on_any_other_project()
    {
        var result = Types
          .InAssembly(ContractsAssembly.GetAssemblyReference())
          .Should()
          .NotHaveDependencyOnAll(
            ApplicationAssembly.GetAssemblyReference().FullName,
            ApiAssembly.GetAssemblyReference().FullName,
            ConsumersAssembly.GetAssemblyReference().FullName,
            DomainAssembly.GetAssemblyReference().FullName,
            PersistenceAssembly.GetAssemblyReference().FullName,
            PresentationAssembly.GetAssemblyReference().FullName,
            TestsAssembly.GetAssemblyReference().FullName)
          .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}