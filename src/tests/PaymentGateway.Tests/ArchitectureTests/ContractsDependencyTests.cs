using FluentAssertions;
using NetArchTest.Rules;
using PaymentGateway.Api;
using PaymentGateway.Application;
using PaymentGateway.Consumers;
using PaymentGateway.Contracts;
using PaymentGateway.Domain;
using PaymentGateway.Persistence;
using PaymentGateway.Presentation;

namespace PaymentGateway.Tests.Application;

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