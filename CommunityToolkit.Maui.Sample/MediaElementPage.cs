#define p4inch
//#define Medium
//#define pixel_7

using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Graphics;
using PaulSchlyter;
using System;
using System.ComponentModel;

namespace CommunityToolkit.Maui.Sample;
public partial class MediaElementPage : BasePage
{
    private readonly MediaElement MediaElement;
    private readonly Image OverlayImage;
    private static Label? TheTextVertical;

    static IDispatcherTimer? timer;

    Label infolabel = new();
    MediaElementState mediaElementState;
    Size size;

    public MediaElementPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
        //Padding = 12;

        //var today = new DateTime(2024, 1, 9);
        Calculator.Calculate(DateTime.Now);

        MediaElement = new MediaElement
        {
            ShouldAutoPlay = true,
            Source = MediaSource.FromResource("solfilmen0_utkast.mp4")
        };
        OverlayImage = new Microsoft.Maui.Controls.Image { Source = "overlay_image.png" };

        BuildGrid();

        MediaElement!.PropertyChanged += MediaElement_PropertyChanged;
        MediaElement!.StateChanged += MediaElementPage_StateChanged;

        MediaElement!.PositionChanged += MediaElementPage_PositionChanged;

        TheTextVertical!.IsVisible = false;
        OverlayImage.IsVisible = false;
        BindingContext = this;
    }

    private void MediaElementPage_PositionChanged(object? sender, EventArgs e)
    {
        TheTextVertical!.IsVisible =
        OverlayImage.IsVisible =
        MediaElement.Position.Between(TimeSpan.FromMilliseconds(9500), TimeSpan.FromMilliseconds(30000));
    }

    private void MediaElementPage_StateChanged(object? sender, Core.Primitives.MediaStateChangedEventArgs e)
    {
        mediaElementState = e.NewState;
        if (e.NewState==MediaElementState.Playing)
        {
            timer = Application.Current!.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(9);
            timer.Tick += (s, e) => DoSomething();
            timer.Start();

        }
        infolabel.Text = ReadDeviceDisplay();

    }
    void DoSomething()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            TheTextVertical!.IsVisible = true;
            OverlayImage.IsVisible = true;
            infolabel.Text = ReadDeviceDisplay();
        });
        timer?.Stop();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        size.Width = width;
        size.Height = height;
        infolabel.Text = ReadDeviceDisplay();
    }

    //TODO: kort2.mp4 with hardcoded positions
    //TODO: solfilmen0_utkast.mp4, timing.
    private string ReadDeviceDisplay()
    {
        System.Text.StringBuilder sb = new();

        sb.AppendLine($"Pixel width: {DeviceDisplay.Current.MainDisplayInfo.Width} / Pixel Height: {DeviceDisplay.Current.MainDisplayInfo.Height}");
        sb.AppendLine($"Density: {DeviceDisplay.Current.MainDisplayInfo.Density}");
        sb.AppendLine($"Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}");
        //sb.AppendLine($"Rotation: {DeviceDisplay.Current.MainDisplayInfo.Rotation}");
        //sb.AppendLine($"Refresh Rate: {DeviceDisplay.Current.MainDisplayInfo.RefreshRate}");
        sb.AppendLine($"Size.Width: {size.Width}");
        sb.AppendLine($"size.Height: {size.Height}");
        sb.AppendLine($"timer: {timer?.IsRunning}");

        return sb.ToString();
    }

    private void OnHeppClicked(object obj)
    {
        TheTextVertical!.IsVisible = !TheTextVertical.IsVisible;
        OverlayImage.IsVisible = !OverlayImage.IsVisible;

    }

    private void OnPlayClicked()
    {
        MediaElement.Play();
    }

    private void OnPauseClicked()
    {
        MediaElement.Pause();
    }

    private void OnStopClicked()
    {
        MediaElement.Stop();
    }

    void MediaElement_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
        {
            //logger.LogInformation("Duration: {NewDuration}", MediaElement.Duration);
        }
    }

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
