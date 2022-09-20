using Circles.Domain;

namespace Circles.Application.Services.Interfaces;

public interface ICircleService
{
    Task<ICollection<Circle>> GetCircles(CancellationToken ct);
    Task<int> SetDescription(int id, string description, CancellationToken ct);
}