using Presentation.Middleware;
using Presentation.SignalR;
using Serilog;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<SignalRNotificationMiddleware>();
app.UseMiddleware<SynchronousRequestMiddleware>();

app.UseSerilogRequestLogging();

app.MapHub<ChatHub>("/chat-hub");

app.Run();