using System.Reflection;

namespace WebApi;

public static class AssemblyReference
{
    public static Assembly AddWebApiAssembly() =>
        typeof(AssemblyReference).Assembly;
}