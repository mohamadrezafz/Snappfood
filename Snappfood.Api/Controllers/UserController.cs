using Microsoft.AspNetCore.Mvc;
using Snappfood.Api.Controllers.Base;
using Snappfood.Application.UseCases.User;

namespace Snappfood.Api.Controllers;

public class UserController : ApiControllerBase
{
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    public UserController(GetAllUsersUseCase getAllUsersUseCase)
    {
        _getAllUsersUseCase = getAllUsersUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _getAllUsersUseCase.ExecuteAsync();
        return Ok(response);
    }
}
