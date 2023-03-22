using FluentAssertions;
using NetArchTest.Rules;
using PaymentGateway.Api;
using PaymentGateway.Application;
using PaymentGateway.Consumers;
using PaymentGateway.Contracts;
using PaymentGateway.Persistence;
using PaymentGateway.Presentation;

namespace PaymentGateway.Tests.Application;

[TestFixture]
public class PersistenceDependencyTests
{
    [Test(Description = "Persistence project command repositories should inherit from CommandRepositoryBase.")]
    [Category("PersistenceDependencyTests")]
    public void Persistence_project_command_repositories_should_inherit_from_CommandRepositoryBase()
    {
        var result = Types
            .InAssembly(PersistenceAssembly.GetAssemblyReference())
            .That()
            .HaveNameEndingWith("CommandRepository")
            .Should()
            .Inherit(typeof(CommandRepositoryBase<>))
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Test(Description = "Persistence project query repositories should inherit from QueryRepositoryBase.")]
    [Category("PersistenceDependencyTests")]
    public void Persistence_project_query_repositories_should_inherit_from_QueryRepositoryBase()
    {
        var result = Types
            .InAssembly(PersistenceAssembly.GetAssemblyReference())
            .That()
            .HaveNameEndingWith("QueryRepository")
            .Should()
            .Inherit(typeof(QueryRepositoryBase))
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Test(Description = "Persistence project should a dependency on Application project.")]
    [Category("PersistenceDependencyTests")]
    public void Persistence_project_should_depend_on_Application_project()
    {
        var result = Types
            .InAssembly(PersistenceAssembly.GetAssemblyReference())
            .That()
            .Inherit(typeof(CommandRepositoryBase<>))
            .Should()
            .HaveDependencyOn(ApplicationAssembly.GetAssemblyReference().FullName)
            .And()
            .NotHaveDependencyOnAll
            (
                ApiAssembly.GetAssemblyReference().FullName,
                ConsumersAssembly.GetAssemblyReference().FullName,
                ContractsAssembly.GetAssemblyReference().FullName,
                PresentationAssembly.GetAssemblyReference().FullName,
                TestsAssembly.GetAssemblyReference().FullName
            )
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
