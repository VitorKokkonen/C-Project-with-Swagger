namespace ProductDomain.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    // Construtor privado para EF
    private Product() { }
    // Factory Method
    public static Product Create(string name, string description, decimal price,
    int stock = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required");
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero");
        return new Product
        {
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Price = price,
            Stock = stock,
            CreatedAt = DateTime.UtcNow
        };
    }
    // Comportamentos de domínio
    public void Update(string name, string description, decimal price)
    {
        Name = name.Trim();
        Description = description?.Trim() ?? string.Empty;
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }
    public void UpdateStock(int quantity)
    {
        if (Stock + quantity < 0)
            throw new InvalidOperationException("Insufficient stock");
        Stock += quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}