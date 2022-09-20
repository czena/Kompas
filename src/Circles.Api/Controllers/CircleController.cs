using Circles.Api.Requests;
using Circles.Api.Responses;
using Circles.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Circles.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public sealed class CircleController: ControllerBase
{
    private readonly ICircleService _circleService;

    public CircleController(ICircleService circleService)
    {
        _circleService = circleService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCircles(CancellationToken ct)
    {
        var circles = await _circleService.GetCircles(ct);
        return Ok(new GetCirclesResponse(circles));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SetDescription(SetDescriptionRequest request, CancellationToken ct)
    {
        var result = await _circleService.SetDescription(request.Id, request.Description, ct);
        if (result == -1) return new BadRequestResult();
        return Ok();
    }
}