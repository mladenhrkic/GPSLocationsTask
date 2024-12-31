using System.Reflection;

namespace Infrastructure;

public static class AssemblyReference
{
    public static Assembly GetAssemblyInfrastructure(Assembly assembly) => 
        typeof(AssemblyReference).Assembly;
}