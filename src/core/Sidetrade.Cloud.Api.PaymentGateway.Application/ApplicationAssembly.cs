using System.Reflection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Application;
public static class ApplicationAssembly
{
    public static Assembly GetAssemblyReference() =>  typeof(ApplicationAssembly).Assembly;
}
