using System.Reflection;

public class DomainAssembly
{
    public static Assembly GetAssemblyReference() => typeof(DomainAssembly).Assembly;
}