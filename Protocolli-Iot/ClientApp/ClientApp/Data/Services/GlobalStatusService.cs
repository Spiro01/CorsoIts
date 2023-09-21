namespace ClientApp.Data.Services;

public class GlobalStatusService
{
    private GlobalStatus _droneStatus;

    public GlobalStatus DroneStatus
    {
        get => _droneStatus;
        set
        {
            _droneStatus = value;

            OnStatusChange.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler OnStatusChange;



}

public enum GlobalStatus
{
    Off,
    On,
    InFlight
}