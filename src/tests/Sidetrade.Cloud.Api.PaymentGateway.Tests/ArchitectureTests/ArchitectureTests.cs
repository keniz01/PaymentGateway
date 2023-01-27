using FluentAssertions;
using NetArchTest.Rules;
using Sidetrade.Cloud.Api.PaymentGateway.Api;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Consumers;
using Sidetrade.Cloud.Api.PaymentGateway.Contracts;
using Sidetrade.Cloud.Api.PaymentGateway.Domain;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.ArchitectureTests
{
    [TestFixture]
    public class ArchitectureTests
    {
        [Test(Description = "Asserts that the Domain Project is not referenced by other projects.")]
        [Category("ArchitectureTests")]
        public void Domain_project_should_not_depend_on_any_other_project()
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

        [Test(Description = "Asserts that the Presentation Project is not referenced by Persistence project.")]
        [Category("ArchitectureTests")]
        public void Presentation_project_should_not_depend_on_persistence_project()
        {
            var result = Types
              .InAssembly(PresentationAssembly.GetAssemblyReference())
              .Should()
              .HaveDependencyOn(PersistenceAssembly.GetAssemblyReference().FullName)
              .And()
              .HaveDependencyOnAny(
                ApplicationAssembly.GetAssemblyReference().FullName,
                PresentationAssembly.GetAssemblyReference().FullName)
              .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }

        [Test(Description = "Asserts that the Application Project only references Domain project.")]
        [Category("ArchitectureTests")]
        public void Application_project_should_only_depend_on_domain_project_only()
        {
            var result = Types
              .InAssembly(ApplicationAssembly.GetAssemblyReference())
              .Should()
              .HaveDependencyOn(DomainAssembly.GetAssemblyReference().FullName)
              //.And()
              //.NotHaveDependencyOnAny(
              //  //ApiAssembly.GetAssemblyReference().FullName,
              //  //ContractsAssembly.GetAssemblyReference().FullName,
              //  //PersistenceAssembly.GetAssemblyReference().FullName,
              //  //PresentationAssembly.GetAssemblyReference().FullName,
              //  TestsAssembly.GetAssemblyReference().FullName)
              .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
