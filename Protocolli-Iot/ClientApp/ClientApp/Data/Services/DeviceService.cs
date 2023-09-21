using System.Diagnostics;
using ClientApp.Interfaces;

namespace ClientApp.Data.Services;

public class DeviceService : IDeviceService
{
    public async Task<Location?> GetCurrentLocation()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
            var location = await Geolocation.Default.GetLocationAsync(request);
            return location;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }
    public double GetBatteryLevel() => Battery.ChargeLevel;
}