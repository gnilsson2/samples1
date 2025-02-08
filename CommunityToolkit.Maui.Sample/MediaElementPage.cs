
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

    Label infolabel = new();

    public MediaElementPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;

        //var today = new DateTime(2024, 1, 9);
        Calculator.Calculate(DateTime.Now);

        MediaElement = new MediaElement
        {
            ShouldAutoPlay = false,
            Source = MediaSource.FromResource("solfilmen0_utkast.mp4")
        };

        BindingContext = this;

        LoadOverlays();

        BuildGrid();

        MediaElement!.PositionChanged += MediaElementPage_PositionChanged;

        OverlayViewRise!.IsVisible = false;

        MediaElement.Play(); // try to make work on Emulators, didnt work!
        MediaElement.Stop();
        MediaElement.Play();
    }


    private void MediaElementPage_PositionChanged(object? sender, EventArgs e)
    {
        OverlayViewRise!.IsVisible = MediaElement.Position.Between(8.9, 30.1);
    }

    private string ReadDeviceDisplay()
    {
        System.Text.StringBuilder sb = new();

        sb.Append($"Pixel width: {DeviceDisplay.Current.MainDisplayInfo.Width} / height: {DeviceDisplay.Current.MainDisplayInfo.Height}");
        sb.AppendLine($"  Density: {DeviceDisplay.Current.MainDisplayInfo.Density}");

        return sb.ToString();
    }

    private void OnHeppClicked(object obj)
    {
        OverlayViewRise!.IsVisible = !OverlayViewRise!.IsVisible;
        infolabel.Text = ReadDeviceDisplay();
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

public abstract class BasePage() : ContentPage { }

public partial class BaseViewModel : ObservableObject { }

public static partial class MyExtensions
{
    public static bool Between(this TimeSpan source, double start, double end)
    {
        if (source.TotalMilliseconds < start*1000) return false;
        if (source.TotalMilliseconds > end*1000) return false;
        return true;
    }
}
