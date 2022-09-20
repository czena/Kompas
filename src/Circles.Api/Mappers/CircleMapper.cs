using Circles.Api.DTOs;
using Circles.Domain;
using Microsoft.AspNetCore.Identity;

namespace Circles.Api.Mappers;

public static class CircleMapper
{
    public static CircleDTO ToDTO(this Circle circle)
    {
        return new CircleDTO(circle.Id, circle.Description, circle.x, circle.y);
    }
}