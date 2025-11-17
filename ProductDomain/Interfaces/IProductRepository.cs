using ProductDomain.Entities;
// ProductDomain/Interfaces/IProductRepository.cs
namespace ProductDomain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(string name);
    Task<bool> ExistsAsync(int id, string name);
}