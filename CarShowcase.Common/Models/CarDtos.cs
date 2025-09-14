namespace CarShowcase.Common.Models;


public class CarDto {
    public string Company { get; set; } = default;
    public string Model { get; set; } = default;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public byte[]? ImageBytes { get; set; }
    public string? ImageMimeType { get; set; }
}
public class CarListItemDto : CarDto
{
    public CarListItemDto(int id, string company, string model, decimal price, string? description, bool hasImage, string? imageDataUrl)
    {
        Id = id;
        Company = company;
        Model = model;
        Price = price;
        Description = description;
        HasImage = hasImage;
        ImageDataUrl = imageDataUrl;
    }

    public int Id { get; set; }
    public bool HasImage { get; set; }
    public string? ImageDataUrl { get; set; }
}
public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int Page, int PageSize);

public class CarDetailDto : CarDto {
    public CarDetailDto(int id, string company, string model, decimal price, string? description, bool hasImage, string? imageDataUrl)
    {
        Id = id;
        Company = company;
        Model = model;
        Price = price;
        Description = description;
        HasImage = hasImage ;
        ImageDataUrl = imageDataUrl;
    }

   public int Id { get; set; }
   public bool HasImage { get; set; }
   public string? ImageDataUrl { get; set; }

}

public class CreateCarDto : CarDto
{
    public CreateCarDto(string company, string model, decimal price, string? description, byte[]? imageBytes, string? mime)
    {
        Company = company;
        Model = model;
        Price = price;
        Description = description;
        ImageBytes = imageBytes;
        ImageMimeType = mime;
    }
}

public class UpdateCarDto : CarDto
{

    public UpdateCarDto(string company, string model, decimal price, string? description, byte[]? imageBytes, string? mime)
    {
        Company = company;
        Model = model;
        Price = price;
        Description = description;
        ImageBytes = imageBytes;
        ImageMimeType = mime;
    }
}


