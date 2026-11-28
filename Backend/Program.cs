
using Microsoft.EntityFrameworkCore;
using Backend.Services.Customer;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// OpenAPI and Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Register customer services
builder.Services.AddScoped<PrescriptionService>();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
