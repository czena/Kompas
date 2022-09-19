using System.Threading;
using System.Threading.Tasks;
using Circles.Api.Requests;
using Circles.Application.Services.Interfaces;
using Circles.Auth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
    public async Task<IActionResult> Login(GenerateTokenRequest request, CancellationToken token)
    {
        var user = await _userService.Get(request.user.Login, request.user.Password, token);
        if (user == null) return Unauthorized();
        var authToken = _authService.GenerateTokenByLogin(_configuration["Jwt:Symmetric:Key"], request.user.Login);
        return Ok(authToken);
    }

    [HttpGet]
    [Authorize]
    public IActionResult ValidateToken() 
    {
        return Ok();
    }
}