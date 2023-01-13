using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITS.Spironelli.Verifica.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        [HttpPost("{id}")]

        public async Task<IActionResult> Post([FromRoute]int id)
        {

            return Ok();
        } 
    }
}
