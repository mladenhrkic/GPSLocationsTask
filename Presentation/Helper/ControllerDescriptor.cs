using GooglePlacesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Presentation.Helper;

public static class ControllerDescriptor
{
    public static ControllerActionDescriptor Get(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var descriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
        
        return descriptor ?? new ControllerActionDescriptor();
    }
}