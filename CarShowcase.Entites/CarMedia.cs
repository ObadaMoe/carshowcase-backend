using CarShowcase.Entities;

namespace CarShowcase.Entities;

public class CarMedia
{
    // Shared PK and FK to Cars (CarId == Cars.Id)
    public int Id { get; set; }

    public byte[]? Image { get; set; }
    public string? ImageMimeType { get; set; }

    // back reference (optional)
    public Car? Car { get; set; }
}