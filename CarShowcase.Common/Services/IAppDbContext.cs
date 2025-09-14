using CarShowcase.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShowcase.Common.Services;

public interface IAppDbContext
{
    DbSet<Car> Cars { get; }
    DbSet<CarMedia> CarMedia { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}


