using System.Collections.Generic;
using System.Threading;

namespace Circles.Domain.Abstractions;

public interface ICircleRepository
{
    IAsyncEnumerable<Circle> Get(CancellationToken token);
}