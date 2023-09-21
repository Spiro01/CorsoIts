using Server.Models;

namespace ApplicationCore.Interfaces.Services;

public interface IControlService
{
    public Task<bool> SendCommand(Control control, Drone drone);
    public Task<bool> BroadCastCommand(Control control);

}