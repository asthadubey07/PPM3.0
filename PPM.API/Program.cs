using PPM.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject DbContext with SQL Server connection
builder.Services.AddDbContext<PPMContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Inject Data Access Layers
builder.Services.AddScoped<ProjectDal>();
builder.Services.AddScoped<RoleDal>();
builder.Services.AddScoped<EmployeeDal>();
builder.Services.AddScoped<ProjectEmployeeDal>();


var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable authentication and authorization if needed
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();