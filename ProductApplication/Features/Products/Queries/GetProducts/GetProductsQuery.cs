// ProductApplication/Features/Products/Queries/GetProducts/GetProductsQuery.cs
using MediatR;
using ProductApplication.Common;
using ProductDomain.Interfaces;
using ProductApplication.DTOs;
using AutoMapper;
namespace ProductApplication.Features.Products.Queries.GetProducts;

public record GetProductsQuery : IRequest<Result<List<ProductDto>>>;
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery,
Result<List<ProductDto>>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request,
    CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync();
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        return Result<List<ProductDto>>.Success(productDtos);
    }
}