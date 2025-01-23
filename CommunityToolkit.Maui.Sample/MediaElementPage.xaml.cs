using System;
using System.ComponentModel;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage<MediaElementViewModel>
{
	readonly ILogger logger;

	public MediaElementPage(MediaElementViewModel viewModel, ILogger<MediaElementPage> logger) : base(viewModel)
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