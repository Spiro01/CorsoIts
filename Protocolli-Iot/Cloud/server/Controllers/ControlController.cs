using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.AspNetCore.AttributeRouting;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControlController : ControllerBase
    {

        private readonly IControlRepository _controlRepository;

        public ControlController( IControlRepository controlRepository)
        {
            _controlRepository = controlRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommands()
        {
            return new OkObjectResult( await _controlRepository.Get());
        }

        [HttpPost]
        public async Task<IActionResult> InsertCommand(Control control)
        {
            return new OkObjectResult(await _controlRepository.Insert(control));
        }

    }
}
