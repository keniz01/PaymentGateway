using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using PaymentGateway.Api;
using PaymentGateway.Presentation;

namespace PaymentGateway.Tests.Application;

[TestFixture]
public class ApiDependencyTests
{
    [Test(Description = "Api Project only depends on Presentation project.")]
    [Category("ApiDependencyTests")]
    public void Api_project_depends_on_presentation_project()
    {
        var result = Types
            .InAssembly(ApiAssembly.GetAssemblyReference())
            .That()
            .Inherit(typeof(ControllerBase))
            .Should()
            .HaveDependencyOn(PresentationAssembly.GetAssemblyReference().FullName)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}