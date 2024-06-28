using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Snappfood.Api.Controllers.Base;
using Snappfood.Application.Models.Request;
using Snappfood.Application.UseCases.Order;
using Snappfood.Application.UseCases.Product;
using Snappfood.Domain.Entities;

namespace Snappfood.Api.Controllers;

public class ProductController : ApiControllerBase
{
    private readonly IValidator<ProductCreate> _createValidator;

    private readonly AddProductUseCase _addProductUseCase;
    private readonly UpdateProductUseCase _updateProductUseCase;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;
    private readonly AddOrderUseCase _addOrderUseCase;
    public ProductController(IValidator<ProductCreate> createValidator ,
        AddProductUseCase addProductUseCase,
        UpdateProductUseCase updateProductUseCase,
        GetProductByIdUseCase getProductByIdUseCase ,
        AddOrderUseCase addOrderUseCase)
    {
        _createValidator = createValidator;

        _addProductUseCase = addProductUseCase;
        _updateProductUseCase = updateProductUseCase;
        _getProductByIdUseCase = getProductByIdUseCase;
        _addOrderUseCase = addOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreate request)
    {
        var validate = _createValidator.Validate(request);
        if (validate.Errors.Count != 0)
            throw new Application.Exceptions.ValidationException(validate.Errors);

        await _addProductUseCase.ExecuteAsync(request);
        return Created();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _getProductByIdUseCase.ExecuteAsync(id);
        return Ok(response);
    }

    [HttpPut("{id}/inventory")]
    public async Task<IActionResult> UpdateInventory(int id, [FromBody] ProductUpdate product)
    {
        if (id != product.Id)
            return BadRequest("Product ID mismatch.");

        await _updateProductUseCase.ExecuteAsync(product);
        return Ok();
    }

    [HttpPost("{id}/buy")]
    public async Task<IActionResult> Buy(int id, [FromBody] OrderCreate request)
    {
        if (id != request.ProductId)
            return BadRequest("Product ID mismatch.");
        await _addOrderUseCase.ExecuteAsync(request);
        return Created();
    }

}
