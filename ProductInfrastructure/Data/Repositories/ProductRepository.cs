// ProductRepository.cs implementa a interface IProductRepository que está na
// camada Domain.
// ProductInfrastructure/Data/Repositories/ProductRepository.cs
using Microsoft.EntityFrameworkCore;
using ProductDomain.Entities;
using ProductDomain.Interfaces;
namespace ProductInfrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    // Buscar por id.
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    // Listar todos.
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
        .OrderBy(p => p.Name)
        .ToListAsync();
    }
    // Adicionar novo.
    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        return product;
    }
    // Atualizar existente.
    public Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }
    // Remover.
    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }
    }
    // Validações de negócio.
    public async Task<bool> ExistsAsync(string name)
    {
        return await _context.Products
        .AnyAsync(p => p.Name.ToLower() == name.ToLower());
    }
    public async Task<bool> ExistsAsync(int id, string name)
    {
        return await _context.Products
        .AnyAsync(p => p.Id != id && p.Name.ToLower() == name.ToLower());
    }
}