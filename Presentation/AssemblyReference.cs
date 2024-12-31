using System.Reflection;

namespace Presentation;

public class AssemblyReference
{
    public static Assembly AddPresentationAssembly() =>
        typeof(AssemblyReference).Assembly;
}