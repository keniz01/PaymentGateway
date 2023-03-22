using System.Reflection;

namespace PaymentGateway.Api
{
    public static class ApiAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(ApiAssembly).Assembly;
    }
}
