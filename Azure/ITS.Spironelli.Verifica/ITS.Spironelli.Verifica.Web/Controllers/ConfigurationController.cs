using ITS.Spironelli.Verifica.Web.Interfaces;
using ITS.Spironelli.Verifica.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITS.Spironelli.Verifica.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService) => _configurationService = configurationService;

        [HttpPost("{id}")]
        public async Task<IActionResult> Post([FromRoute] int id, [FromBody] PanelConfiguration panelConfiguration)
        {
            var result = await _configurationService.ChangePanelConfiguration(id, panelConfiguration);

            if (result) return NoContent();
            return BadRequest();
        }
    }
}
