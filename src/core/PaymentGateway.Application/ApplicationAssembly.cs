using System.Reflection;

namespace PaymentGateway.Application;
public static class ApplicationAssembly
{
    public static Assembly GetAssemblyReference() =>  typeof(ApplicationAssembly).Assembly;
}
