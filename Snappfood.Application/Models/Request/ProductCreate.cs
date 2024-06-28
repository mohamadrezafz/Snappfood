
using Snappfood.Domain.Entities;

namespace Snappfood.Application.Models.Request;

public class ProductCreate
{
    public string Title { get; set; } = default!;
    public uint InventoryCount { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; } //  percentage
}
