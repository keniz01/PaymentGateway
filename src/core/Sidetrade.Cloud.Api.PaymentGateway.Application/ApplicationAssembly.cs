using System.Reflection;

public class ApplicationAssembly
{
    public static Assembly GetAssemblyReference() =>  typeof(ApplicationAssembly).Assembly;
}