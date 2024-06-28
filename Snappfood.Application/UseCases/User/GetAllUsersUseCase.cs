
using Snappfood.Application.Interfaces.Repositories;
using Snappfood.Application.Models.Response;

namespace Snappfood.Application.UseCases.User;

public class GetAllUsersUseCase
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponse>> ExecuteAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => new UserResponse
        {
            Id = user.Id,
            Name = user.Name
        }).ToList();
    }
}
