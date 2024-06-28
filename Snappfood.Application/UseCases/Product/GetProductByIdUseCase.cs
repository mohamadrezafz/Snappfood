using Snappfood.Application.Constants;
using Snappfood.Application.Exceptions;
using Snappfood.Application.Interfaces.Caching;
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Response;

namespace Snappfood.Application.UseCases.Product;

public class GetProductByIdUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductCacheService _productCacheService;

    public GetProductByIdUseCase(IProductRepository productRepository, IProductCacheService productCacheService)
    {
        _productRepository = productRepository;
        _productCacheService = productCacheService;
    }

    public async Task<ProductResponse> ExecuteAsync(int id)
    {
        var product = _productCacheService.GetProductById(id);
        if (product != null)
            return (ProductResponse)product;

        var entity = await _productRepository.GetByIdAsync(id);
        if (entity != null)
        {
            _productCacheService.SetProduct(entity);
            return (ProductResponse)entity;
        }
        else
            throw new NotFoundException(ExceptionMessages.ProductNotFound);
    }


}