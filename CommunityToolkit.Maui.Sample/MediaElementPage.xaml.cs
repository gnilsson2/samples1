using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage<BaseViewModel>
{
    readonly ILogger logger;

    public MediaElementPage(BaseViewModel viewModel, ILogger<MediaElementPage> logger) : base(viewModel)
    {
        InitializeComponent();

        this.logger = logger;
        MediaElement.PropertyChanged += MediaElement_PropertyChanged;
    }

    void MediaElement_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == MediaElement.DurationProperty.PropertyName)
        {
            logger.LogInformation("Duration: {NewDuration}", MediaElement.Duration);
        }
    }

    void OnMediaOpened(object? sender, EventArgs e) => logger.LogInformation("Media opened.");
    void OnStateChanged(object? sender, MediaStateChangedEventArgs e) => logger.LogInformation("Media State Changed. Old State: {PreviousState}, New State: {NewState}", e.PreviousState, e.NewState);
    void OnMediaFailed(object? sender, MediaFailedEventArgs e) => logger.LogInformation("Media failed. Error: {ErrorMessage}", e.ErrorMessage);
    void OnMediaEnded(object? sender, EventArgs e) => logger.LogInformation("Media ended.");
    void OnPositionChanged(object? sender, MediaPositionChangedEventArgs e) => logger.LogInformation("Position changed to {Position}", e.Position);
    void OnSeekCompleted(object? sender, EventArgs e) => logger.LogInformation("Seek completed.");
    void OnPlayClicked(object? sender, EventArgs e) => MediaElement.Play();
    void OnPauseClicked(object? sender, EventArgs e) => MediaElement.Pause();
    void OnStopClicked(object? sender, EventArgs e) => MediaElement.Stop();

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        MediaElement.Stop();
        MediaElement.Handler?.DisconnectHandler();
    }
}

public abstract class BasePage<TViewModel>(TViewModel viewModel) : BasePage(viewModel)
    where TViewModel : BaseViewModel
{
    public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

public abstract class BasePage : ContentPage
{
    protected BasePage(object? viewModel = null)
    {
        BindingContext = viewModel;
        Padding = 12;

        if (string.IsNullOrWhiteSpace(Title))
        {
            Title = GetType().Name;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Debug.WriteLine($"OnAppearing: {Title}");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        Debug.WriteLine($"OnDisappearing: {Title}");
    }
}