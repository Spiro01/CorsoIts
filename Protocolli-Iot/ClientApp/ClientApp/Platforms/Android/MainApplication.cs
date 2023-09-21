using Android;
using Android.App;
using Android.Runtime;

[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesFeature("android.hardware.location", Required = true)]
[assembly: UsesFeature("android.hardware.location.gps", Required = true)]
[assembly: UsesFeature("android.hardware.location.network", Required = true)]
[assembly: UsesPermission(Manifest.Permission.AccessBackgroundLocation)]
[assembly: UsesPermission(Manifest.Permission.BatteryStats)]
namespace ClientApp;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}
	
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
