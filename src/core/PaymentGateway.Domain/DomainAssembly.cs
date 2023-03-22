using System.Reflection;

namespace PaymentGateway.Domain
{
    public static class DomainAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(DomainAssembly).Assembly;
    }
}
