

using Snappfood.Domain.Entities;

namespace Snappfood.Application.Models.Response;

public class ProductResponse
{

    public static explicit operator ProductResponse(Domain.Entities.Product input) => new()
    {
        CreationDate = input.CreationDate,
        Discount = input.Discount,
        InventoryCount = input.InventoryCount,
        Price = input.Price - (input.Price * (input.Discount / 100)),
        OriginalPrice = input.Price ,
        Title = input.Title,
        Id = input.Id,
    };
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public uint InventoryCount { get; set; }
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal Discount { get; set; }
    public DateTime CreationDate { get; set; }
}
