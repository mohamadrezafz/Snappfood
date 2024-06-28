using Snappfood.Domain.Common;

namespace Snappfood.Domain.Entities;

public class Order : BaseEntity
{
    public Order()
    {
        
    }
    public Order(int productId , int buyerId)
    {
        ProductId = productId;
        BuyerId = buyerId;
        CreationDate = DateTime.Now;
    }
    public int ProductId { get;   set; }
    public Product Product { get;  set; } = default!;
    public DateTime CreationDate { get;  set; }
    public int BuyerId { get;  set; }
    public User Buyer { get;  set; } = default!;
}
