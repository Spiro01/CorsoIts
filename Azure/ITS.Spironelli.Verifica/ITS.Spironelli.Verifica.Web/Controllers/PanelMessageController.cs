using ITS.Spironelli.Verifica.Web.Interfaces;
using ITS.Spironelli.Verifica.Domain.Entities;
using ITS.Spironelli.Verifica.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITS.Spironelli.Verifica.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelMessageController : ControllerBase
    {
        private readonly IPanelAdsService _panelAdsService;

        public PanelMessageController(IPanelAdsService panelAdsService)
        {
            _panelAdsService = panelAdsService;
        }

        [HttpPost("{id}")]

        public async Task<IActionResult> Post([FromRoute]int id, [FromBody] PanelMessage panelMessage)
        {
            var result = await _panelAdsService.SetPanelMessage(id,panelMessage);

            if (result) return NoContent();
            return BadRequest();
        } 
    }
}
