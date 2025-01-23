using Android.App;
using Android.Content.PM;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.Sample.Platforms.Android;

[Activity(Theme = "@style/MainTheme", ResizeableActivity = true, MainLauncher = true, LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : MauiAppCompatActivity
{

}