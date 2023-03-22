using FluentAssertions;
using MediatR;
using NetArchTest.Rules;
using PaymentGateway.Application;
using PaymentGateway.Domain;
using PaymentGateway.Persistence;
using PaymentGateway.Presentation;

namespace PaymentGateway.Tests.Application;
public class PresentationDependencyTests
{
    [Test(Description = "Asserts that the Presentation Project is not referenced by Application project.")]
    [Category("PresentationArchitectureTests")]
    public void Assert_Presentation_project_should_depend_on_application_project()
    {
        var result = Types
            .InAssembly(PresentationAssembly.GetAssemblyReference())
                .That()
                .ImplementInterface(typeof(IMediator))
            .Should()
            .HaveDependencyOn(ApplicationAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Test(Description = "Asserts that the Presentation Project is not referenced by Persistence project.")]
    [Category("PresentationArchitectureTests")]
    public void Assert_Presentation_project_does_not_depend_on_domain_project()
    {
        var result = Types
            .InAssembly(PresentationAssembly.GetAssemblyReference())
            .Should()
            .NotHaveDependencyOn(DomainAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

        [Test(Description = "Asserts that the Presentation Project is not referenced by Persistence project.")]
    [Category("PresentationArchitectureTests")]
    public void Assert_Presentation_project_should_not_depend_on_Persistence_project()
    {
        var result = Types
            .InAssembly(PresentationAssembly.GetAssemblyReference())
            .Should()
            .NotHaveDependencyOn(PersistenceAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}