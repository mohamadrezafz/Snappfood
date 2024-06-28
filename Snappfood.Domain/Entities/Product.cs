using Snappfood.Domain.Common;

namespace Snappfood.Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
            
    }
    public Product(string title , uint inventoryCount , decimal price , decimal discount)
    {
        Title = title;
        InventoryCount = inventoryCount;
        Price = price;
        Discount = discount;
        CreationDate = DateTime.Now;
    }

    public string Title { get;  set; } = default!;
    public uint InventoryCount { get;  set; }
    public decimal Price { get;  set; }
    public decimal Discount { get;  set; } //  percentage
    public DateTime CreationDate { get;  set; }
    public DateTime UpdatedDate { get;  set; }
    public ICollection<Order> Orders { get;  set; } = new List<Order>();
}
