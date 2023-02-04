using FluentAssertions;
using NetArchTest.Rules;
using Sidetrade.Cloud.Api.PaymentGateway.Application;
using Sidetrade.Cloud.Api.PaymentGateway.Domain;
using Sidetrade.Cloud.Api.PaymentGateway.Persistence;
using Sidetrade.Cloud.Api.PaymentGateway.Presentation;

namespace Sidetrade.Cloud.Api.PaymentGateway.Tests.Application;
public class PresentationDependencyTests
{
    [Test(Description = "Asserts that the Presentation Project is not referenced by Application project.")]
    [Category("ArchitectureTests")]
    public void Assert_Presentation_project_does_not_depend_on_application_project()
    {
        var result = Types
            .InAssembly(PresentationAssembly.GetAssemblyReference())
            .Should()
            .HaveDependencyOn(PersistenceAssembly.GetAssemblyReference().FullName)
            .And()
            .NotHaveDependencyOn(ApplicationAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Test(Description = "Asserts that the Presentation Project is not referenced by Persistence project.")]
    [Category("ArchitectureTests")]
    public void Assert_Presentation_project_does_not_depend_on_domain_project()
    {
        var result = Types
            .InAssembly(PresentationAssembly.GetAssemblyReference())
            .Should()
            .HaveDependencyOn(PersistenceAssembly.GetAssemblyReference().FullName)
            .And()
            .NotHaveDependencyOn(DomainAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}