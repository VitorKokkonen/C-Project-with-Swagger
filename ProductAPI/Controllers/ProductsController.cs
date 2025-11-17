using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Features.Products.Commands.CreateProduct;
using ProductApplication.Features.Products.Queries.GetProducts;
namespace ProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetProductsQuery());
        return result.IsSuccess
        ? Ok(result.Value)
        : BadRequest(result.Error);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess
        ? Ok(new { Id = result.Value })
        : BadRequest(result.Error);
    }
}