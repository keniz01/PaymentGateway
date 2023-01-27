using FluentAssertions;
using NetArchTest.Rules;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Domain;
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
                "Sidetrade.Cloud.Api.PaymentGateway.Application",
                "Sidetrade.Cloud.Api.PaymentGateway.Api",
                "Sidetrade.Cloud.Api.PaymentGateway.EventConsumer",
                "Sidetrade.Cloud.Api.PaymentGateway.EventMessages",
                "Sidetrade.Cloud.Api.PaymentGateway.Persistence",
                "Sidetrade.Cloud.Api.PaymentGateway.Presentation",
                "Sidetrade.Cloud.Api.PaymentGateway.Tests")
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
              .HaveDependencyOn("Sidetrade.Cloud.Api.PaymentGateway.Persistence")
              .And()
              .HaveDependencyOnAny(
                "Sidetrade.Cloud.Api.PaymentGateway.Application",
                "Sidetrade.Cloud.Api.PaymentGateway.Presentation")
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
              .HaveDependencyOn("Sidetrade.Cloud.Api.PaymentGateway.Domain")
              .And()
              .NotHaveDependencyOnAny(
                "Sidetrade.Cloud.Api.PaymentGateway.Api",
                "Sidetrade.Cloud.Api.PaymentGateway.EventConsumer",
                "Sidetrade.Cloud.Api.PaymentGateway.EventMessages",
                "Sidetrade.Cloud.Api.PaymentGateway.Persistence",
                "Sidetrade.Cloud.Api.PaymentGateway.Presentation",
                "Sidetrade.Cloud.Api.PaymentGateway.Tests")
              .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
