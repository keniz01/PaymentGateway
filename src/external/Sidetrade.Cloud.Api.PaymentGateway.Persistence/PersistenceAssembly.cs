using System.Reflection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence;

public static class PersistenceAssembly
{
    public static Assembly GetAssemblyReference() => typeof(PersistenceAssembly).Assembly;
}
