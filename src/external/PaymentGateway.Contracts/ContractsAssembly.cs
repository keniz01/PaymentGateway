using System.Reflection;

namespace PaymentGateway.Contracts
{
    public static class ContractsAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(ContractsAssembly).Assembly;
    }
}
