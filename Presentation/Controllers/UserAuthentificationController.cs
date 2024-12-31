using Application.Categories.Authentification;
using Domain.Models;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Extensions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAuthentificationController(IMediator mediator, 
    ILogger<UserAuthentificationController> logger, INotificationService notificationService) : ControllerBase
{
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        try
        {
            if (IsEmptyFields(user))
            {
                return BadRequest("Invalid user credentials.");
            }

            var model = new UserAuthentication{ User = user };
            var result = await mediator.Send(model);
            
            return result.IsSuccess
                ? Ok($"ApiKey: {result.Value?.Key}")
                : ResultExtensions.ToProblemDetails(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpPost("Registration")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        try
        {
            if (IsEmptyFields(user))
            {
                return BadRequest("Invalid user credentials.");
            }
            
            var result = await mediator.Send(new RegistrationAuthentication { User = user });
            return result.IsSuccess
                ? Ok()
                : ResultExtensions.ToProblemDetails(result);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    private static bool IsEmptyFields(User user)
    {
        return string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password);
    }
    private ActionResult HandleError(Exception ex)
    {
        logger.LogError($"An error occurred: {ex.Message}");
        return StatusCode(500, new { message = "An error occurred while processing your request." });
    }
}