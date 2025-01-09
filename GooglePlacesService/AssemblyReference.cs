using System.Reflection;

namespace GooglePlacesService;

public static class AssemblyReference
{
    public static Assembly AddGooglePlaceAssembly(Assembly assembly) => 
        typeof(AssemblyReference).Assembly;
}