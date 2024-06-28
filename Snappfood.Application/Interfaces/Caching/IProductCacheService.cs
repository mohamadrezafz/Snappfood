
using Snappfood.Domain.Entities;

namespace Snappfood.Application.Interfaces.Caching;

public interface IProductCacheService
{
    void ClearCache(int id);
    Product GetProductById(int id);
    void SetProduct(Product product);   
     
}
