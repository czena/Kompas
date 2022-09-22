using Circles.Api.DTOs;
using Circles.Domain;

namespace Circles.Api.Mappers;

internal static class CircleMapper
{
    public static CircleDTO ToDTO(this Circle circle)
    {
        return new CircleDTO(circle.Id, circle.X, circle.Y);
    }
}