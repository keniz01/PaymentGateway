using System.Reflection;

namespace PaymentGateway.Consumers
{
    public static class ConsumersAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(ConsumersAssembly).Assembly;
    }
}
