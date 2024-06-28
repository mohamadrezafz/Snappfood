

namespace Snappfood.Application.Models.Response;

public class UserResponse
{
    public static explicit operator UserResponse(Domain.Entities.User input) => new()
    {
        Name = input.Name,
        Id = input.Id,
    };
    public string Name { get; set; } = default!;
    public int Id { get; set; }
}
