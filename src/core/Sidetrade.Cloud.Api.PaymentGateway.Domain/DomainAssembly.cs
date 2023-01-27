using System.Reflection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Domain
{
    public static class DomainAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(DomainAssembly).Assembly;
    }
}
