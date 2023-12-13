using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

