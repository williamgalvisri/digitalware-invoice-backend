using ApiDigitalWare.Core.Interface;
using ApiDigitalWare.Infrastructure.Models;
using ApiDigitalWare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<db_system_digitalwareContext>(options =>
{
    var _builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
    IConfiguration configuration = _builder.Build();
    // Get value from app settings
    var connectionString = configuration.GetValue<string>("ConnectionStrings:dbDigitalware");
    if (connectionString != null)
    {
        connectionString = connectionString.Trim();
        options.UseSqlServer(connectionString);
    }
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod(); ;
        }
    );
});
// Add Interface
builder.Services.AddTransient<CustomerInterface, CustomerRepositories>();
builder.Services.AddTransient<InvoiceInterface, InvoiceRepositories>();
builder.Services.AddTransient<ProductInterface, ProductRepositories>();
builder.Services.AddScoped<InventorieRepositories>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();


app.MapControllers();

app.Run();
