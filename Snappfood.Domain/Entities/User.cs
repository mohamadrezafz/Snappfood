using Snappfood.Domain.Common;
namespace Snappfood.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
            
    }

    public User(int id , string name )
    {
        Id = id;
        Name = name;
    }
    public string Name { get;  set; } = default!;
    public ICollection<Order> Orders { get;  set; } = new List<Order>();
}
