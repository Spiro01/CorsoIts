using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : IBasicCloudController<User, Guid?>
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid? id)
    {
        return new OkObjectResult(!id.HasValue ? await _userRepository.Get() : await _userRepository.Get(id.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(User entity)
    {
        return new OkObjectResult(_userRepository.Insert(entity));
    }

    [HttpPut]
    public async Task<IActionResult> Update(User entity)
    {
        return new OkObjectResult(_userRepository.Update(entity));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(User entity)
    {
        return new OkObjectResult(_userRepository.Delete(entity.Id));
    }
}