using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
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

        var positionLabel = new Label
        {
            HorizontalOptions = LayoutOptions.Start
        };
        positionLabel.SetBinding(Label.TextProperty, new Binding("Position", source: MediaElement));
        var durationLabel = new Label
        {
            HorizontalOptions = LayoutOptions.End,
            HorizontalTextAlignment = TextAlignment.End
        };
        durationLabel.SetBinding(Label.TextProperty, new Binding("Duration", source: MediaElement));
        var infoGrid = new Microsoft.Maui.Controls.Grid
        {
            HorizontalOptions = LayoutOptions.Fill
        };
        infoGrid.Children.Add(positionLabel);
        infoGrid.Children.Add(durationLabel);
        Thegrid.Children.Add(infoGrid);
        Thegrid.SetRow(infoGrid, 3);


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