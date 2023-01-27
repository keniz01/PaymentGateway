using System.Reflection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Api
{
    public static class ApiAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(ApiAssembly).Assembly;
    }
}
