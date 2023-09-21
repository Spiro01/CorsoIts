using ClientApp.Data.Services;
using ClientApp.Interfaces;

namespace ClientApp;

public partial class MainPage : ContentPage
{
    private readonly GlobalStatusService _globalStatus;
    private readonly IStatusRepository _statusRepository;
    private readonly IDeviceService _deviceService;

    private CancellationTokenSource _cancelDelay;

    public MainPage(GlobalStatusService globalStatus, IStatusRepository statusRepository, IDeviceService deviceService)
    {
        _globalStatus = globalStatus;
        _statusRepository = statusRepository;
        _deviceService = deviceService;
        _globalStatus.OnStatusChange += _globalStatus_OnStatusChange;
        _cancelDelay = new CancellationTokenSource();
        InitializeComponent();
        Start();
    }

    private void _globalStatus_OnStatusChange(object? sender, EventArgs e)
    {
        StatusLabel.Text = _globalStatus.DroneStatus.ToString();
        _cancelDelay.Cancel();
    }

    private void PowerOnBtn_OnClicked(object? sender, EventArgs e)
    {
        if (_globalStatus.DroneStatus == GlobalStatus.Off)
        {

            _globalStatus.DroneStatus = GlobalStatus.On;
            StartBtn.IsEnabled = true;
        }
        else
        {
            _globalStatus.DroneStatus = GlobalStatus.Off;
            StartBtn.IsEnabled = false;
        }

    }

    private void StartBtn_OnClicked(object? sender, EventArgs e)
    {
        if (_globalStatus.DroneStatus == GlobalStatus.On)
        {
            _globalStatus.DroneStatus = GlobalStatus.InFlight;
            PowerOnBtn.IsEnabled = false;
        }
        else
        {
            _globalStatus.DroneStatus = GlobalStatus.On;
            PowerOnBtn.IsEnabled = true;
        }

    }

    private async void Start()
    {
       
        await Task.Run(async () =>
        {

            while (true)
            {
                var ct = _cancelDelay.Token;
                try
                {
                    switch (_globalStatus.DroneStatus)
                    {
                        case GlobalStatus.Off:
                            await Task.Delay(2000, ct);
                            break;
                        case GlobalStatus.On:
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                var position = await _deviceService.GetCurrentLocation();
                                var batteryLevel = _deviceService.GetBatteryLevel();
                                await _statusRepository.PublishStatus(position, batteryLevel);
                                ChangeUi(position);
                            });

                            await Task.Delay(60000, ct);
                            break;
                        case GlobalStatus.InFlight:
                            MainThread.BeginInvokeOnMainThread(async () =>
                            {
                                var position = await _deviceService.GetCurrentLocation();
                                var batteryLevel = _deviceService.GetBatteryLevel();
                                await _statusRepository.PublishStatus(position, batteryLevel);
                                ChangeUi(position);
                            });
                            await Task.Delay(1000, ct);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _cancelDelay.Dispose();
                    _cancelDelay = new CancellationTokenSource();
                }
            }
        });
    }

    private void ChangeUi(Location location)
    {
        LongitudeLabel.Text = location.Longitude.ToString();
        LatitudeLabel.Text = location.Latitude.ToString();
        AccuracyLabel.Text = location.Accuracy.ToString();
    }
}

