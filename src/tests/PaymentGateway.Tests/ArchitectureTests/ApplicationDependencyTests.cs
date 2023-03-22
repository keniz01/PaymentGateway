using FluentAssertions;
using NetArchTest.Rules;
using PaymentGateway.Api;
using PaymentGateway.Application;
using PaymentGateway.Application.Abstractions.Commands;
using PaymentGateway.Contracts;
using PaymentGateway.Domain;
using PaymentGateway.Persistence;
using PaymentGateway.Presentation;

namespace PaymentGateway.Tests.Application;

[TestFixture]
public class ApplicationDependencyTests
{
    [Test(Description = "Asserts that the Application Project only references Domain project.")]
    [Category("ApplicationDependencyTests")]
    public void Assert_Application_project_depends_on_domain_project()
    {
        var result = Types
            .InAssembly(ApplicationAssembly.GetAssemblyReference())
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveDependencyOn(DomainAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Test(Description = "Asserts that the Application Project only references Domain project and not any other projects.")]
    [Category("ApplicationDependencyTests")]
    public void Assert_Application_project_should_not_depend_on_others_projects_but_domain_project()
    {
        var result = Types
            .InAssembly(ApplicationAssembly.GetAssemblyReference())
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .NotHaveDependencyOnAll(
                ApiAssembly.GetAssemblyReference().FullName,
                ContractsAssembly.GetAssemblyReference().FullName,
                PersistenceAssembly.GetAssemblyReference().FullName,
                PresentationAssembly.GetAssemblyReference().FullName,
                TestsAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}