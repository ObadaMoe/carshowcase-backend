using CarShowcase.Common.Models;
using CarShowcase.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShowcase.Common.Services;

public class CarService(IAppDbContext db) : ICarService
{
    public async Task<PagedResult<CarListItemDto>> SearchAsync(string? query, int page, int pageSize, CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 10);

        var baseQuery = db.Cars.AsNoTracking().Include(c => c.Media).AsQueryable();
        if (!string.IsNullOrWhiteSpace(query))
        {
            var term = query.ToLower();
            baseQuery = baseQuery.Where(c =>
                (c.Company + " " + c.Model + " " + (c.Description ?? "") + " " + c.Price)
                .ToLower().Contains(term));
        }

        var total = await baseQuery.CountAsync(ct);
        var items = await baseQuery
            .OrderByDescending(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CarListItemDto(
                c.Id,
                c.Company,
                c.Model,
                c.Price,
                c.Description,
                c.Media != null && c.Media.Image != null,
                c.Media != null && c.Media.Image != null && !string.IsNullOrWhiteSpace(c.Media.ImageMimeType)
                    ? $"data:{c.Media.ImageMimeType};base64,{Convert.ToBase64String(c.Media.Image)}"
                    : null))
            .ToListAsync(ct);

        return new PagedResult<CarListItemDto>(items, total, page, pageSize);
    }

    public async Task<CarDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var c = await db.Cars.AsNoTracking().Include(x => x.Media).FirstOrDefaultAsync(x => x.Id == id, ct);
        if (c is null) return null;
        return new CarDetailDto(
            c.Id,
            c.Company,
            c.Model,
            c.Price,
            c.Description,
            c.Media != null && c.Media.Image != null,
                c.Media != null && c.Media.Image != null && !string.IsNullOrWhiteSpace(c.Media.ImageMimeType)
                    ? $"data:{c.Media.ImageMimeType};base64,{Convert.ToBase64String(c.Media.Image)}"
                    : null);
    }


    public async Task<int> CreateAsync(CreateCarDto dto, CancellationToken ct = default)
    {
        var car = new Car
        {
            Company = dto.Company,
            Model = dto.Model,
            Price = dto.Price,
            Description = dto.Description
        };
        db.Cars.Add(car);
        await db.SaveChangesAsync(ct);

        if (dto.ImageBytes is not null && !string.IsNullOrWhiteSpace(dto.ImageMimeType))
        {
            db.CarMedia.Add(new CarMedia { Id = car.Id, Image = dto.ImageBytes, ImageMimeType = dto.ImageMimeType });
            await db.SaveChangesAsync(ct);
        }
        return car.Id;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCarDto dto, CancellationToken ct = default)
    {
        var car = await db.Cars.Include(c => c.Media).FirstOrDefaultAsync(c => c.Id == id, ct);
        if (car is null) return false;

        car.Company = dto.Company;
        car.Model = dto.Model;
        car.Price = dto.Price;
        car.Description = dto.Description;

        if (dto.ImageBytes is not null && !string.IsNullOrWhiteSpace(dto.ImageMimeType))
        {
            if (car.Media is null)
                db.CarMedia.Add(new CarMedia { Id = car.Id, Image = dto.ImageBytes, ImageMimeType = dto.ImageMimeType });
            else
            {
                car.Media.Image = dto.ImageBytes;
                car.Media.ImageMimeType = dto.ImageMimeType;
            }
        }

        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var car = await db.Cars.FindAsync([id], ct);
        if (car is null) return false;
        db.Cars.Remove(car);
        await db.SaveChangesAsync(ct);
        return true;
    }
}


