using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Server.Interfaces.IRepository;
using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase
{
    private readonly ILogger<StatusController> _logger;
    private readonly IStatusRepository _statusRepository;

    public StatusController(ILogger<StatusController> logger, IStatusRepository statusRepository)
    {
        _logger = logger;
        _statusRepository = statusRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string? DroneId, string? fromDate)
    {
        if (fromDate is not null && !DateTime.TryParse(fromDate, out _))return new BadRequestObjectResult("Bad date format") ;

        var result = await _statusRepository.Get(DroneId, fromDate);

        return new OkObjectResult(result);

    }


    [HttpPost]
    public async Task<IActionResult> Insert(Status status)
    {

        if (status is null) return new BadRequestResult();
        await _statusRepository.Insert(status);
        return new OkObjectResult("ok");




    }

    [HttpDelete]

    public async Task<IActionResult> Delete()
    {
        await _statusRepository.Purge();
        return new OkObjectResult("ok");
    }

}