// CarShowcase.Api/Controllers/CarsController.cs
using CarShowcase.Common.Models;
using CarShowcase.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarShowcase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController(ICarService carService) : ControllerBase
{
    // GET /api/cars?q=&page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<PagedResult<CarListItemDto>>> Get(
        [FromQuery] string? q, [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await carService.SearchAsync(q, page, pageSize, ct);
        return Ok(result);
    }

    // GET /api/cars/5
    [HttpGet("CarInfo-{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => await carService.GetByIdAsync(id) is { } car
           ? Ok(car)
           : NotFound();



    // POST /api/cars  (multipart/form-data)
    [HttpPost("AddCar-{company}-{model}")]
    [RequestSizeLimit(10_000_000)] // 10MB, adjust as needed
    public async Task<IActionResult> Create([FromForm] CarRequest req)
    {
        byte[]? imageBytes = null; string? mime = null;
        if (req.Image is { Length: > 0 })
        {
            using var ms = new MemoryStream();
            await req.Image.CopyToAsync(ms);
            imageBytes = ms.ToArray();
            mime = req.Image.ContentType;
        }

        var id = await carService.CreateAsync(new Common.Models.CreateCarDto(
            req.Company, req.Model, req.Price, req.Description, imageBytes, mime));

        return CreatedAtAction(nameof(GetById), new { id }, new { id, req.Company, req.Model, req.Price, req.Description });
    }

    // PUT /api/cars/5  (multipart/form-data so you can replace image too)
    [HttpPut("UpdateCar-{id:int}-{company}-{model}")]
    [RequestSizeLimit(10_000_000)]
    public async Task<IActionResult> Update(int id, [FromForm] CarRequest req)
    {
        byte[]? imageBytes = null; string? mime = null;
        if (req.Image is { Length: > 0 })
        {
            using var ms = new MemoryStream();
            await req.Image.CopyToAsync(ms);
            imageBytes = ms.ToArray();
            mime = req.Image.ContentType;
        }

        var ok = await carService.UpdateAsync(id, new Common.Models.UpdateCarDto(
            req.Company, req.Model, req.Price, req.Description, imageBytes, mime));
        return ok ? NoContent() : NotFound();
    }

    // DELETE /api/cars/5
    [HttpDelete("DeleteCar-{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await carService.DeleteAsync(id) ? NoContent() : NotFound();

    public class CarRequest {
        public string Company { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }

}
