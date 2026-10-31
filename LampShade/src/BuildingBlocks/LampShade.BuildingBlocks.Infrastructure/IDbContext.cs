using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LampShade.BuildingBlocks.Infrastructure;

public interface IDbContext
{
    ChangeTracker ChangeTracker { get; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}