using CarShowcase.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShowcase.Common.Services;

public interface IAppDbContext
{
    DbSet<Car> Cars { get; }
    DbSet<CarMedia> CarMedia { get; } 
    DbSet<PaymentDetails> PaymentDetails { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}


