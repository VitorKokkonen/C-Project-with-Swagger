using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.Features.Products.Commands.CreateProduct;
using ProductApplication.Features.Products.Queries.GetProducts;
using ProductApplication.Features.Products.Queries.GetProductById;
using ProductApplication.Features.Products.Commands.UpdateProduct;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
        return result.IsSuccess
        ? Ok(result.Value)
        : NotFound(result.Error);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        // Garantir que o ID da rota seja usado
        var updateCommand = command with { Id = id };
        var result = await _mediator.Send(updateCommand);
        return result.IsSuccess
        ? Ok(new { Message = "Product updated successfully" })
        : BadRequest(result.Error);
    }
}