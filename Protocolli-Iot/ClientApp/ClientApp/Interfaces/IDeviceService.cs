namespace ClientApp.Interfaces;

public interface IDeviceService
{
    Task<Location?> GetCurrentLocation();
    double GetBatteryLevel();
}