using MediatR;
using ProductApplication.Common;
using ProductDomain.Interfaces;
using ProductApplication.DTOs;
using AutoMapper;
namespace ProductApplication.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery : IRequest<Result<ProductDto>>
{
    public int Id { get; init; }
}
public class GetProductByIdQueryHandler :
IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetProductByIdQueryHandler(IProductRepository
    productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery
    request, CancellationToken cancellationToken)
    {
        var product = await
        _productRepository.GetByIdAsync(request.Id);
        if (product == null)
            return Result<ProductDto>.Failure("Product not found");
        var productDto = _mapper.Map<ProductDto>(product);
        return Result<ProductDto>.Success(productDto);
    }
}