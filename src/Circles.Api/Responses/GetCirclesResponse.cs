using Circles.Api.DTOs;

namespace Circles.Api.Responses;

public record GetCirclesResponse(ICollection<CircleDTO> Circles);