//ProductApplication/Features/Products/Commands/CreateProduct/CreateProductCommand.cs
using MediatR;
using ProductApplication.Common;
using FluentValidation;
using ProductDomain.Interfaces;
using ProductDomain.Entities;
namespace ProductApplication.Features.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Result<int>>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Stock { get; init; }
}
public class CreateProductCommandValidator :
AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required")
        .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        RuleFor(x => x.Description)
        .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        RuleFor(x => x.Price)
        .GreaterThan(0).WithMessage("Price must be greater than zero");
        RuleFor(x => x.Stock)
        .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");
    }
}
public class CreateProductCommandHandler :
IRequestHandler<CreateProductCommand, Result<int>>
{
    // Variáveis recebidas via construtor (Injeção de Dependência)
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    // Construtor recebe dependências necessárias.
    public CreateProductCommandHandler(IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    // Método principal, executa o caso de uso.
    public async Task<Result<int>> Handle(CreateProductCommand request,
    CancellationToken cancellationToken)
    {
        // Validação de regra de negócio
        if (await _productRepository.ExistsAsync(request.Name))
            return Result<int>.Failure("Product with this name already exists");
        var product = Product.Create(request.Name, request.Description,
        request.Price, request.Stock);
        // Persistência no banco de dados
        await _productRepository.AddAsync(product);
        await _unitOfWork.CommitAsync(cancellationToken);
        // Retorno do resultado.
        return Result<int>.Success(product.Id);
    }
}