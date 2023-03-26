using ITS.QZER.Test1.Api.Controllers.Servicies;
using ITS.QZER.Test1.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITS.QZER.Test1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LogController : ControllerBase
    {
        private readonly IlogService _log;
        public LogController(IlogService logservice)
        {
            _log = logservice;
        }
        
        [HttpGet]
        public IEnumerable<Log> Get()
        {
            return _log.GetAll(); 
        }
        [HttpGet("GetPrograms")]
        public IEnumerable<string> GetAllPrograms()
        {
            return _log.GetAllPrograms();
        }
        
        [HttpGet("{code}")]
        public IEnumerable<Log> Get_ByCode(string code)
        {
            return _log.GetLogByCode(code);
        }
        
    }

}
