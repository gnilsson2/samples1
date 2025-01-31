
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics.Platform;
using PaulSchlyter;
using System;
using System.IO;
using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{
    private readonly MediaElement MediaElement;
    private static IImage? OverlayImage;
    private static Label? TheTextVertical;
    GraphicsView? graphicsView;

    Label infolabel = new();

    public MediaElementPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
        //Padding = 12;

        //var today = new DateTime(2024, 1, 9);
        Calculator.Calculate(DateTime.Now);

        MediaElement = new MediaElement
        {
            ShouldAutoPlay = true,
            Source = MediaSource.FromResource("kort1.mp4")
        };

        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly!.GetManifestResourceStream("CommunityToolkit.Maui.Sample.Resources.Images.overlay_image.png")!)
        {
            OverlayImage = PlatformImage.FromStream(stream);
        }

        BuildGrid();

        MediaElement!.PositionChanged += MediaElementPage_PositionChanged;

        graphicsView!.IsVisible = false;

        BindingContext = this;
    }

    private void MediaElementPage_PositionChanged(object? sender, EventArgs e)
    {
        //graphicsView!.IsVisible = MediaElement.Position.Between(TimeSpan.FromMilliseconds(9500), TimeSpan.FromMilliseconds(30000));
    }

    //TODO: solfilmen0_utkast.mp4, timing.
    private string ReadDeviceDisplay()
    {
        System.Text.StringBuilder sb = new();

        sb.AppendLine($"Pixel width: {DeviceDisplay.Current.MainDisplayInfo.Width} / Pixel Height: {DeviceDisplay.Current.MainDisplayInfo.Height}");
        sb.AppendLine($"Density: {DeviceDisplay.Current.MainDisplayInfo.Density}");
        sb.AppendLine($"Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}");

        return sb.ToString();
    }

    private void OnHeppClicked(object obj)
    {
        graphicsView!.IsVisible = !graphicsView!.IsVisible;
    }

    private void OnPlayClicked() => MediaElement.Play();

    private void OnPauseClicked() => MediaElement.Pause();

    private void OnStopClicked() => MediaElement.Stop();

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        MediaElement.Stop();
        MediaElement.Handler?.DisconnectHandler();
    }

}

public abstract class BasePage() : ContentPage
{

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    Debug.WriteLine($"OnAppearing: {Title}");
    //}

}

public partial class BaseViewModel : ObservableObject
{
}

public static partial class MyExtensions
{
    public static bool Between(this TimeSpan source, TimeSpan start, TimeSpan end)
    {
        return source >= start && source <= end;
    }
}
