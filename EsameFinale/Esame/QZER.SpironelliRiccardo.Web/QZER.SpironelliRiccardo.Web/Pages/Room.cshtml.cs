using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Devices.Common.Exceptions;
using QZER.SpironelliRiccardo.Web.Interfaces;
using QZER.SpironelliRiccardo.Web.Models;
using QZER.SpironelliRiccardo.Web.Repository;
using QZER.SpironelliRiccardo.Web.Services;

namespace QZER.SpironelliRiccardo.Web.Pages
{
    public class RoomModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IIotHubService _iotHubService;
        private readonly ILogger<RoomModel> _logger;


        public RoomModel(IRoomRepository roomRepository, IIotHubService iotHubService, ILogger<RoomModel> logger)
        {
            _roomRepository = roomRepository;
            _iotHubService = iotHubService;
            _logger = logger;
        }

        [BindProperty]
        public Room SelectedRoom { get; private set; }
        public async Task OnGet(Guid id)
        {
            SelectedRoom = await _roomRepository.GetRoomAsync(id) ?? new Room();

        }
        public async Task<IActionResult> OnPost(Guid roomId)
        {

            var room = await _roomRepository.GetRoomAsync(roomId);
            if (room is not null)
            {

                var message = new IotHubMessage
                {
                    Timeout = room.Timeout <= 255 && room.Timeout >=0 ? room.Timeout : 255,
                };
                try
                {
                    await _iotHubService.SendIotHubMessage(room.GatewayId!, message);
                }
                catch (DeviceNotFoundException ex)
                {
                    //TODO Hande error
                    _logger.LogError(ex.Message);
                }
                SelectedRoom = room;
                return Page();
            }
            
            return Redirect("/");
        }
    }
}
