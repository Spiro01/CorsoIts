using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QZER.SpironelliRiccardo.Web.Interfaces;
using QZER.SpironelliRiccardo.Web.Models;

namespace QZER.SpironelliRiccardo.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;

        [BindProperty]
        public IEnumerable<Room> Room { get; set; }

        public IndexModel(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task OnGet()
        {
            Room = await _roomRepository.GetRoomAsync();
        }
        
    }
}