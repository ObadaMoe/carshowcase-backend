using CarShowcase.Entities;

using Microsoft.EntityFrameworkCore;

namespace CarShowcase.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<CarMedia> CarMedia => Set<CarMedia>();

    public DbSet<PaymentDetail> PaymentDetails => Set<PaymentDetail>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Car>(e =>
        {
            e.ToTable("Cars");
            e.Property(p => p.Company).IsRequired().HasMaxLength(100);
            e.Property(p => p.Model).IsRequired().HasMaxLength(100);
            e.Property(p => p.Price).HasColumnType("decimal(10,2)");
            e.HasIndex(p => new { p.Company, p.Model });
        });

        b.Entity<CarMedia>(e =>
        {
            e.ToTable("CarMedia");

            // PK is Id
            e.HasKey(m => m.Id);

            // 1:1 with Car
            e.HasOne(m => m.Car)
             .WithOne(c => c.Media)
             .HasForeignKey<CarMedia>(m => m.Id)   // shared PK
             .OnDelete(DeleteBehavior.Cascade);
        });

        b.Entity<PaymentDetail>(e =>
        {
            e.ToTable("PaymentDetails");
            e.HasKey(p => p.PaymentDetailId);
            e.Property(p => p.CardOwnerName).IsRequired().HasMaxLength(100);
            e.Property(p => p.CardNumber).IsRequired().HasMaxLength(16);
            e.Property(p => p.CardExpirationDate).IsRequired().HasMaxLength(5);
            e.Property(p => p.SecurityCode).IsRequired().HasMaxLength(3);
        });
    }

}