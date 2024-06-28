using Snappfood.Application.Constants;
using Snappfood.Application.Exceptions;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Request;

namespace Snappfood.Application.UseCases.Product;

public class UpdateProductUseCase
{
    private readonly IProductRepository _productRepository;

    public UpdateProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(ProductUpdate product)
    {
        var existingProduct = await _productRepository.GetByIdAsync(product.Id);
        if (existingProduct == null)
            throw new NotFoundException(ExceptionMessages.ProductNotFound);

        if(existingProduct.InventoryCount > product.InventoryCount)
            throw new Exception(ExceptionMessages.InventoryCountShouldIncrease);

        existingProduct.InventoryCount = product.InventoryCount;
        existingProduct.UpdatedDate = DateTime.UtcNow;
        await _productRepository.UpdateAsync(existingProduct);
    }
}