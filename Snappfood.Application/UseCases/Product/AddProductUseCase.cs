
using Snappfood.Application.Constants;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Request;

namespace Snappfood.Application.UseCases.Product;

public class AddProductUseCase
{
    private readonly IProductRepository _productRepository;

    public AddProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ExecuteAsync(ProductCreate product)
    {
        var existingProduct = await _productRepository.GetByTitleAsync(product.Title);
        if (existingProduct != null)
            throw new Exception(ExceptionMessages.ProductTitleIsDuplicate);

        var entity = new Domain.Entities.Product(product.Title, product.InventoryCount, product.Price, product.Discount);
        await _productRepository.AddAsync(entity);
    }
}