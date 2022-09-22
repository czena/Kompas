using Circles.Application.Services.Interfaces;
using Circles.Domain;
using Circles.Domain.Abstractions;

namespace Circles.Application.Services;

public class CircleService: ICircleService
{
    private ICircleRepository _circleRepository;

    public CircleService(ICircleRepository circleRepository)
    {
        _circleRepository = circleRepository;
    }

    public async Task<ICollection<Circle>> GetCircles(CancellationToken ct)
    {
        return await _circleRepository.Get(ct).ToArrayAsync(cancellationToken: ct);
    }

    public async Task<int> SetDescription(int id, string description, CancellationToken ct)
    {
        return await _circleRepository.SetDescription(id, description, ct);
    }

    public async Task<string?> GetDescription(int id, CancellationToken ct)
    {
        return await _circleRepository.GetDescription(id, ct);
    }
}