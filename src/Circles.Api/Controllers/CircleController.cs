using Circles.Api.Mappers;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCirclesResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCircles(CancellationToken ct)
    {
        var circles = await _circleService.GetCircles(ct);
        if (circles.Count == 0) return new NotFoundResult();
        return Ok(new GetCirclesResponse(circles.Select(a => a.ToDTO()).ToArray()));
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SetDescription(SetDescriptionRequest request, CancellationToken ct)
    {
        var result = await _circleService.SetDescription(request.Id, request.Description, ct);
        if (result == -1) return new BadRequestResult();
        return Ok();
    }
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDescription(GetDescriptionRequest request, CancellationToken ct)
    {
        var result = await _circleService.GetDescription(request.Id, ct);
        if (result == null) return new NotFoundResult();
        return Ok(result);
    }
}