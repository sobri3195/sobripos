using Microsoft.EntityFrameworkCore;
using SOBRIPOS.Application.Interfaces;
using SOBRIPOS.Data;
using SOBRIPOS.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SOBRIPOS API", Version = "v1", Description = "SOBRIPOS Point of Sale System API" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString) || connectionString.Contains("UseSqlServer"))
    {
        options.UseSqlServer(connectionString ?? "Server=localhost;Database=SOBRIPOS;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    else
    {
        options.UseSqlite(connectionString);
    }
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SOBRIPOS API v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
