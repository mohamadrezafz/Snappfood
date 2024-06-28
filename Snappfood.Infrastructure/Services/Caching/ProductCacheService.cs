
using Snappfood.Application.Interfaces.Caching;
using Snappfood.Domain.Entities;

namespace Snappfood.Infrastructure.Services.Caching;

public class ProductCacheService : IProductCacheService
{
    private readonly ICacheProvider _cacheProvider;
    public ProductCacheService(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public void ClearCache(int id) =>
        _cacheProvider.ClearCache(id.ToString());

    public Product GetProductById(int id) =>
         _cacheProvider.GetFromCache<Product>(id.ToString()) ;

    public void SetProduct(Product product) =>
           _cacheProvider.SetCache(product.Id.ToString(), product, DateTimeOffset.Now.AddHours(12));
}
