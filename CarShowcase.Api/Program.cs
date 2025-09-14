// Program.cs (Api)
using CarShowcase.Api.Data;
using Microsoft.EntityFrameworkCore;
using CarShowcase.Api.Services;
using CarShowcase.Common.Services;

var builder = WebApplication.CreateBuilder(args);


const string CorsPolicy = "AngularCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(CorsPolicy, p =>
        p.WithOrigins("http://localhost:4200")
         .AllowAnyHeader()
         .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IAppDbContext, AppDbContextAdapter>();
builder.Services.AddScoped<ICarService, CarShowcase.Common.Services.CarService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.UseHttpsRedirection();
app.UseCors(CorsPolicy);
app.MapControllers();
app.Run();
