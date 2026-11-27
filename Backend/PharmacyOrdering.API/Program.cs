
using Microsoft.EntityFrameworkCore;
using PharmacyOrdering.API.Data;
using PharmacyOrdering.API.Repositories;
using PharmacyOrdering.API.Repositories.Interfaces;
using PharmacyOrdering.API.Services;
using PharmacyOrdering.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Database connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Repository registrations
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderCancellationRepository, OrderCancellationRepository>();

// Service registrations
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderCancellationService, OrderCancellationService>();

builder.Services.AddControllers();

// Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
