
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using PaulSchlyter;
using System;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{
    private readonly MediaElement MediaElement;
    private readonly Image OverlayImage;
    private static Label? TheTextVertical;

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
        OverlayImage = new Microsoft.Maui.Controls.Image { Source = "overlay_image.png" };

        BuildGrid();

        MediaElement!.PositionChanged += MediaElementPage_PositionChanged;

        //TheTextVertical!.IsVisible = false;
        //OverlayImage.IsVisible = false;
        TheTextVertical!.IsVisible = true;
        OverlayImage.IsVisible = true;


        BindingContext = this;
    }

    private void MediaElementPage_PositionChanged(object? sender, EventArgs e)
    {
        //TheTextVertical!.IsVisible =
        //OverlayImage.IsVisible =
        //MediaElement.Position.Between(TimeSpan.FromMilliseconds(9500), TimeSpan.FromMilliseconds(30000));
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
        TheTextVertical!.IsVisible = !TheTextVertical.IsVisible;
        OverlayImage.IsVisible = !OverlayImage.IsVisible;

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
