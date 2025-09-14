using CarShowcase.Common.Models;
using CarShowcase.Entities;

namespace CarShowcase.Common.Services;

public interface ICarService
{
    Task<PagedResult<CarListItemDto>> SearchAsync(string? query, int page, int pageSize, CancellationToken ct = default);
    Task<CarDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CreateAsync(CreateCarDto dto, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, UpdateCarDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}


