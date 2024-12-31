using System.Text.Json;
using Application.Categories.Command;
using Application.Categories.Queries;
using Application.Categories.Service;
using Domain.Abstractions;
using Domain.Models;
using Domain.Services;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Presentation.DTO;
using Presentation.Extensions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]

public class LocationController(IMediator mediator, IHttpClientFactory httpFactory,
    IConfiguration configuration, INotificationService notificationService, 
    ILogger<UserAuthentificationController> logger)
    : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IActionResult> CreateLocation([FromBody] GeoCoordinate coordinate)
    {
        try
        {
            var model = new LocationServiceMediator
            {
                HttpClientFactory = httpFactory,
                Configuration = configuration,
                GeoCoordinate = coordinate
            };
            var getLocationsFromServices = await mediator.Send(model);
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey);
            
            var result = await mediator.Send(
                new CreateLocation { Locations = getLocationsFromServices, ApiKey = apiKey}
                );
            
            var locationDto = result.Value.Adapt<List<LocationDto>>();
            
            await notificationService.NotifyUserAsync(JsonSerializer.Serialize(result.Value));
            
            return result.IsSuccess
                ? Ok(locationDto)
                : ResultExtensions.ToProblemDetails(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
    
    [HttpPost("SetFavourite")]
    public async Task<IActionResult> SetFavourite([FromBody] List<LocationId> locationId)
    {
        HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey);
        var model = new SetFavourite
        {
            LocationId = locationId,
            ApiKey = apiKey
        };
        
        var result = await mediator.Send(model);
        return result.IsSuccess
            ? Ok()
            : ResultExtensions.ToProblemDetails(result);
    }
    
    [HttpGet("FilterByFavourite")]
    public async Task<IActionResult> GetLocationByFavourite()
    {
        try
        {
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey);
            var result = await mediator.Send(new GetLocationByFavourite{ ApiKey = apiKey });
            var locationDto = result.Value.Adapt<List<LocationDto>>();
            
            await notificationService.NotifyUserAsync(JsonSerializer.Serialize(locationDto));
            
            return result.IsSuccess
                ? Ok(locationDto)
                : ResultExtensions.ToProblemDetails(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    
    [HttpGet("FilterByStreet")]
    public async Task<IActionResult> GetLocationsByStreet([FromQuery] string street)
    {
        try
        {
            var result = await mediator.Send(new GetLocationByStreets{ Street = street });
            var locationDto = result.Value.Adapt<List<LocationDto>>();
            
            await notificationService.NotifyUserAsync(JsonSerializer.Serialize(locationDto));
            
            return result.IsSuccess
                ? Ok(locationDto)
                : ResultExtensions.ToProblemDetails(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
    
    [HttpGet("FilterByCity")]
    public async Task<IActionResult> GetLocationsByCity([FromQuery] string city)
    {
        try
        {
            var result = await mediator.Send(new GetLocationByCity{ City = city });
            var locationDto = result.Value.Adapt<List<LocationDto>>();
            
            await notificationService.NotifyUserAsync(JsonSerializer.Serialize(locationDto));
            
            return result.IsSuccess
                ? Ok(locationDto)
                : ResultExtensions.ToProblemDetails(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpDelete("RemoveFavourite")]
    public async Task<IActionResult> RemoveFavourite([FromBody] List<LocationId> locationId)
    {
        try
        {
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKey);
            var model = new RemoveFavourite { LocationId = locationId, ApiKey = apiKey };
            var result = await mediator.Send(model);
            
            return result.IsSuccess
                ? Ok()
                : ResultExtensions.ToProblemDetails(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }
    
    private IActionResult HandleError(Exception ex)
    {
        logger.LogError($"An error occurred: {ex.Message}");
        
        var result = Result<ApiKey>.Failure(Error.Failure("Location.Failure", 
            $"{ ex.Message }"));
        
        return ResultExtensions.ToProblemDetails(result);
    }
}