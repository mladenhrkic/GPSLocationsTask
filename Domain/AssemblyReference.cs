using System.Reflection;

namespace Domain;

public static class AssemblyReference
{
    public static Assembly GetAssemblyDomain(Assembly assembly) => 
        typeof(AssemblyReference).Assembly;
}