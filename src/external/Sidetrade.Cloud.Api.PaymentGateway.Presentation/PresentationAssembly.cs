using System;
using System.Reflection;

namespace Sidetrade.Cloud.Api.PaymentGateway.Presentation
{
	public static class PresentationAssembly
	{
        public static Assembly GetAssemblyReference() => typeof(PresentationAssembly).Assembly;
    }
}

