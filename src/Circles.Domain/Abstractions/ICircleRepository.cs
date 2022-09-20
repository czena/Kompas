namespace Circles.Domain.Abstractions;

public interface ICircleRepository
{
    IAsyncEnumerable<Circle> Get(CancellationToken ct);
    Task<int> SetDescription(int id, string description, CancellationToken ct);
}