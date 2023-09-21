using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Controllers;

public class RentController : ControllerBase, IBasicCloudController<Rent, Guid?>
{
    private readonly IRentRepository _rentRepository;

    public RentController(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid? id)
    {
        return new OkObjectResult(id.HasValue ? await _rentRepository.Get() : await _rentRepository.Get(id.Value));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(Rent entity)
    {
        var result = await _rentRepository.Insert(entity);
        return new OkObjectResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Rent entity)
    {
        var result = await _rentRepository.Update(entity);
        return new OkObjectResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Rent entity)
    {
        return new OkObjectResult(_rentRepository.Delete(entity.Id));
    }
}