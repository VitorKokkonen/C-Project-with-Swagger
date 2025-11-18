using MediatR;
using ProductApplication.Common;
using FluentValidation;
using ProductDomain.Interfaces;
using ProductDomain.Entities;
namespace ProductApplication.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Result<bool>>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
}
public class UpdateProductCommandValidator :
AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0).WithMessage("Id is required");
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required")
        .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        RuleFor(x => x.Description)
        .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        RuleFor(x => x.Price)
        .GreaterThan(0).WithMessage("Price must be greater than zero");
    }
}
public class UpdateProductCommandHandler :
IRequestHandler<UpdateProductCommand, Result<bool>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateProductCommandHandler(IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<bool>> Handle(UpdateProductCommand request,
    CancellationToken cancellationToken)
    {
        // Buscar produto existente
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
            return Result<bool>.Failure("Product not found");
        // Validar se nome já existe (excluindo o próprio produto)
        if (await _productRepository.ExistsAsync(request.Id, request.Name))
            return Result<bool>.Failure("Another product with this name already exists");
            // Atualizar produto
            product.Update(request.Name, request.Description, request.Price);
        // Persistir mudanças
        await _productRepository.UpdateAsync(product);
        await _unitOfWork.CommitAsync(cancellationToken);
        return Result<bool>.Success(true);
    }
}