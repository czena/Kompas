using Circles.Api.Requests;
using Circles.Api.Responses;
using Circles.Application.Services.Interfaces;
using Circles.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Circles.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController: ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IConfiguration configuration, IAuthService authService, IUserService userService)
    {
        _configuration = configuration;
        _authService = authService;
        _userService = userService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken token)
    {
        var user = await _userService.Get(request.Login, request.Password, _configuration["User:Salt"], token);
        if (user == null) return Unauthorized();
        var authToken = _authService.GenerateTokenByLogin(_configuration["Jwt:Symmetric:Key"], request.Login);
        return Ok(new LoginResponse(authToken));
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult ValidateToken() 
    {
        return Ok();
    }
}