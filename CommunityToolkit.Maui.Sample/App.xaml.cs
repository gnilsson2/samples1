using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

namespace CommunityToolkit.Maui.Sample;

public partial class App : Application
{
	readonly MediaElementPage appShell;

	public App(MediaElementPage appShell)
	{
		InitializeComponent();

		this.appShell = appShell;
	}

	protected override Window CreateWindow(IActivationState? activationState) => new(appShell);
}