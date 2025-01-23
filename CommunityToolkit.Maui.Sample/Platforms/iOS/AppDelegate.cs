using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System.Diagnostics.CodeAnalysis;
namespace CommunityToolkit.Maui.Sample;

[Register(nameof(AppDelegate))]
[RequiresUnreferencedCode($"{nameof(MauiProgram.CreateMauiApp)} requires unreferenced code")]
public class AppDelegate : MauiUIApplicationDelegate
{
#pragma warning disable IL2046
    [RequiresUnreferencedCode($"{nameof(MauiProgram.CreateMauiApp)} requires unreferenced code")]
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
#pragma warning restore IL2046
}