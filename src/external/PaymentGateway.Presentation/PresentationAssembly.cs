using System;
using System.Reflection;

namespace PaymentGateway.Presentation
{
	public static class PresentationAssembly
	{
        public static Assembly GetAssemblyReference() => typeof(PresentationAssembly).Assembly;
    }
}

