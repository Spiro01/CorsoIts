using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QZER.SpironelliRiccardo.ApplicationCore.Interfaces.Services;
using QZER.SpironelliRiccardo.ApplicationCore.Services;
using QZER.SpironelliRiccardo.Data.Entities;
using QZER.SpironelliRiccardo.Data.Repository;
using QZER.SpironelliRiccardo.Domain.Entities;

namespace Qzer.SpironelliRiccardo.Web.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    private readonly IBoardRepository _boardRepository;
    private readonly IWelcomeMessageService _welcomeMessageService;
    [BindProperty]
    public List<Board> Board { get; set; }
    public IndexModel(ILogger<IndexModel> logger, IBoardRepository boardRepository, IWelcomeMessageService welcomeMessageService)
    {
        _logger = logger;
        _boardRepository = boardRepository;
        _welcomeMessageService = welcomeMessageService;
    }

    public async Task OnGet()
    {
        Board =(await _boardRepository.GetBoard()).ToList();
    }

    public async Task<IActionResult> OnPost()
    {
        var message = Request.Form["message"].FirstOrDefault();


        int.TryParse(Request.Form["deviceAddress"].FirstOrDefault(), out int deviceAddress);
        DateTime.TryParse(Request.Form["selectedRoom"].FirstOrDefault() ?? DateTime.Now.Add(new TimeSpan(0,0,30,0)).ToString("R"), out DateTime expiration);

        var welcomeMessage = new WelcomeMessage()
        {
            Message = message ?? string.Empty,
            DeviceAddress = deviceAddress,
            Expiration = expiration

        };

        await _welcomeMessageService.SetWelcomeMessage(welcomeMessage);
        return Page();
    }
}
