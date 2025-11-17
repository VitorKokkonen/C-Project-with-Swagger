using ProductInfrastructure.Data;
using ProductApplication.Common;
using ProductApplication.Features.Products.Commands.CreateProduct;
using ProductInfrastructure.Data.Repositories;
using ProductDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Database - MySQL
var connectionString = "Server=127.0.0.1;Database=ProductDb;Uid=root;Pwd=123456;Port=3306;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 33)) // ajusta se teu MySQL for outra versão
    )
);
// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// MediatR
builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
// AutoMapper
builder.Services.AddAutoMapper((serviceProvider, cfg) =>
{
    cfg.AddProfile<MappingProfile>();
}, typeof(MappingProfile).Assembly);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();