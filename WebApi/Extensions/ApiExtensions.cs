using Application;
using Infrastructure;
using Presentation;
using Serilog;

namespace WebApi.Extensions;

public static class ApiExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var configurationString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

        builder.Services.AddApplication()
            .AddInfrastructure(configurationString)
            .AddPresentation();
        
        var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
        builder.Services.AddControllers().AddApplicationPart(presentationAssembly);
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpClient();
        
        builder.Host.UseSerilog((context, configuration) => 
            configuration.ReadFrom.Configuration(context.Configuration));

    }
}