using CarShowcase.Api.Data;
using CarShowcase.Common.Services;
using CarShowcase.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShowcase.Api.Services;

public class AppDbContextAdapter(AppDbContext inner) : IAppDbContext
{
    public DbSet<Car> Cars => inner.Cars;
    public DbSet<CarMedia> CarMedia => inner.CarMedia;
    public DbSet<PaymentDetails> PaymentDetails => inner.PaymentDetails;
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => inner.SaveChangesAsync(ct);
}


