namespace ClientApp.Interfaces;

public interface IStatusRepository
{
    Task PublishStatus(Location position, double batteryLevel);
}