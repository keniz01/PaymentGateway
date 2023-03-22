using System.Reflection;

namespace PaymentGateway.Persistence;

public static class PersistenceAssembly
{
    public static Assembly GetAssemblyReference() => typeof(PersistenceAssembly).Assembly;
}
