using System.Reflection;

namespace PaymentGateway.Tests
{
    public static class TestsAssembly
    {
        public static Assembly GetAssemblyReference() => typeof(TestsAssembly).Assembly;
    }
}
