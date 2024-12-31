using System.Reflection;

namespace Application;

public static class AssemblyReference
{
    public static Assembly AddApplicationAssembly(Assembly assembly) => 
        typeof(AssemblyReference).Assembly;
}