using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces.IRepository;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LaunchCommand : ControllerBase
    {
        private readonly IDroneRepository _droneRepository;
        private readonly IControlRepository _controlRepository;
        private readonly IControlService _controlService;

        public LaunchCommand(IDroneRepository droneRepository, IControlRepository controlRepository, IControlService controlService)
        {
            _droneRepository = droneRepository;
            _controlRepository = controlRepository;
            _controlService = controlService;
        }

        [HttpPost]
        public async Task<IActionResult> SetCommand(string DroneId, Guid ControlId)
        {
            var drone = await _droneRepository.Get(DroneId);
            var control = await _controlRepository.Get(ControlId);

            if (drone is null || control is null) return new NotFoundObjectResult("Drone or control not found");

            var result = await _controlService.SendCommand(control, drone);

            return new OkObjectResult(new { result });
        }
    }
}
