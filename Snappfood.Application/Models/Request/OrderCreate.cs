

namespace Snappfood.Application.Models.Request;

public class OrderCreate
{
    public int ProductId { get; set; }
    public int BuyerId { get; set; }
}
