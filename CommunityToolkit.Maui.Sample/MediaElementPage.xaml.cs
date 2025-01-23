using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{
    public MediaElementPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
        //Padding = 12;

        InitializeComponent();

        MediaElement.PropertyChanged += MediaElement_PropertyChanged;
    }

    void MediaElement_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
        {
            //logger.LogInformation("Duration: {NewDuration}", MediaElement.Duration);
        }
    }

    void OnPlayClicked(object? sender, EventArgs e) => MediaElement.Play();
    void OnPauseClicked(object? sender, EventArgs e) => MediaElement.Pause();
    void OnStopClicked(object? sender, EventArgs e) => MediaElement.Stop();

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        MediaElement.Stop();
        MediaElement.Handler?.DisconnectHandler();
    }
}

public abstract class BasePage : ContentPage
{
    public BasePage(object? viewModel = null)
    {
    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    Debug.WriteLine($"OnAppearing: {Title}");
    //}

}

public partial class BaseViewModel : ObservableObject
{
}