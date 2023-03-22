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
public class DomainDependencyTests
{
        [Test(Description = "Asserts that the Domain Project is not referenced by other projects.")]
        [Category("DomainArchitectureTests")]
        public void Assert_Domain_project_should_not_depend_on_any_other_project()
        {
            var result = Types
              .InAssembly(DomainAssembly.GetAssemblyReference())
              .Should()
              .NotHaveDependencyOnAll(
                ApplicationAssembly.GetAssemblyReference().FullName,
                ApiAssembly.GetAssemblyReference().FullName,
                ConsumersAssembly.GetAssemblyReference().FullName,
                ContractsAssembly.GetAssemblyReference().FullName,
                PersistenceAssembly.GetAssemblyReference().FullName,
                PresentationAssembly.GetAssemblyReference().FullName,
                TestsAssembly.GetAssemblyReference().FullName)
              .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
}
