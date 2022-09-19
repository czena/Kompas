namespace Circles.Domain.Abstractions;

public interface ICircleRepository
{
    IAsyncEnumerable<Circle> Get(CancellationToken token);
}