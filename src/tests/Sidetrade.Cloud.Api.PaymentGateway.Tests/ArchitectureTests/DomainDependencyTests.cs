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
public class DomainDependencyTests
{
        [Test(Description = "Asserts that the Domain Project is not referenced by other projects.")]
        [Category("DomainArchitectureTests")]
        public void Assert_Domain_project_should_not_depend_on_any_other_project()
        {
            var result = Types
              .InAssembly(DomainAssembly.GetAssemblyReference())
              .Should()
              .NotHaveDependencyOnAny(
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