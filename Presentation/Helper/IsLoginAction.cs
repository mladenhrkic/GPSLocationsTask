using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Presentation.Helper;

public static class IsLoginAction
{
    public static bool Check(HttpContext context)
    {
        var descriptor = ControllerDescriptor.Get(context);
        var allowedActions = new[] { "Login", "Register" };
        return descriptor is { ControllerName: "UserAuthentification" } 
               && allowedActions.Contains(descriptor.ActionName);
    }
}