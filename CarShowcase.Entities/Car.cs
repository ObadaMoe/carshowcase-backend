// CarShowcase.Domain/Entities/Car.cs
using CarShowcase.Entities;

namespace CarShowcase.Entities;

public class Car
{
    public int Id { get; set; }
    public string Company { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public CarMedia? Media { get; set; }
}
