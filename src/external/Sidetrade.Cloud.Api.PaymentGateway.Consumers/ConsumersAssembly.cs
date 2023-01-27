using System.Reflection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Consumers
{
    public static class ConsumersAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(ConsumersAssembly).Assembly;
    }
}
