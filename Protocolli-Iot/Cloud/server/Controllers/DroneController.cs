using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Controllers;
[ApiController]
[Route("[controller]")]
public class DroneController : ControllerBase, IBasicCloudController<Drone, string?>
{
    private readonly IDroneRepository _droneRepository;
    public DroneController(IDroneRepository droneRepository)
    {
        _droneRepository = droneRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? id)
    {
        return new OkObjectResult(id is null ? await _droneRepository.Get() : await _droneRepository.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Insert(Drone entity)
    {
        return new OkObjectResult(await _droneRepository.Insert(entity));
    }
    [NonAction]
    public async Task<IActionResult> Update(Drone entity)
    {
        throw new NotImplementedException();
    }
    [NonAction]
    public async Task<IActionResult> Delete(Drone entity)
    {
        throw new NotImplementedException();
    }
}