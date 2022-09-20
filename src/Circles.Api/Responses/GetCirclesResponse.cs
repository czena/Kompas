using Circles.Domain;

namespace Circles.Api.Responses;

public record GetCirclesResponse(ICollection<Circle> Circles);