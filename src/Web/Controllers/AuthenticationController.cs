using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Infrastructure.Services;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly AuthenticationService _authService;

    public AuthenticationController(AuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        try
        {
            var token = _authService.Authenticate(user.UserName, user.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        var created = _authService.Register(user);
        if (created == null)
            return BadRequest("El usuario ya existe.");

        return Ok(created);
    }
}
