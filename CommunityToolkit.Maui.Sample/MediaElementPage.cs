using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;

namespace CommunityToolkit.Maui.Sample;
public partial class MediaElementPage : BasePage
{
    private MediaElement MediaElement;
    public MediaElementPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
        //Padding = 12;


        MediaElement = new MediaElement
        {
            ShouldAutoPlay = true,
            Source = MediaSource.FromResource("kort1.mp4")
        };

        BuildGrid();

        MediaElement!.PropertyChanged += MediaElement_PropertyChanged;
    }

    private void BuildGrid()
    {
        Microsoft.Maui.Controls.Grid Thegrid;
        Thegrid = new Microsoft.Maui.Controls.Grid
        {
            RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(250) },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
        };


        Thegrid.AddAtRow(0, MediaElement);

        Thegrid.AddAtRow(1, Addstate(Thegrid));

        Thegrid.AddAtRow(2, AddButtons(Thegrid));

        Thegrid.AddAtRow(3, AddPosition(Thegrid));

        Content = Thegrid;
    }

    private Grid AddButtons(Grid Thegrid)
    {
        var buttonGrid = new Microsoft.Maui.Controls.Grid
        {
            Padding = new Thickness(0, 10, 0, 10),
            ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
            ColumnSpacing = 5
        };
        Button b0 = new() { Text = "Play", Command = new Command(OnPlayClicked) };
        buttonGrid.Children.Add(b0);
        buttonGrid.SetColumn(b0, 0);
        Button b1 = new() { Text = "Pause", Command = new Command(OnPauseClicked) };
        buttonGrid.Children.Add(b1);
        buttonGrid.SetColumn(b1, 1);
        Button b2 = new() { Text = "Stop", Command = new Command(OnStopClicked) };
        buttonGrid.Children.Add(b2);
        buttonGrid.SetColumn(b2, 2);

        return buttonGrid;
    }


    private Grid AddPosition(Grid Thegrid)
    {
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

        return infoGrid;
    }

    private HorizontalStackLayout Addstate(Grid Thegrid)
    {
        var horizontalStackLayout = new HorizontalStackLayout
        {
            Padding = new Thickness(0, 0, 0, 15),
            HorizontalOptions = LayoutOptions.Center
        };
        var label = new Label
        {
            HorizontalOptions = LayoutOptions.Center
        };
        var multiBinding = new MultiBinding { StringFormat = "Current State: {0} - Dimensions: {1}x{2}" };
        multiBinding.Bindings.Add(new Binding("CurrentState", source: MediaElement));
        multiBinding.Bindings.Add(new Binding("MediaWidth", source: MediaElement));
        multiBinding.Bindings.Add(new Binding("MediaHeight", source: MediaElement));
        label.SetBinding(Label.TextProperty, multiBinding);
        horizontalStackLayout.Children.Add(label);

        return horizontalStackLayout;
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

public static class MyExtensions
{

    public static void AddAtRow(this Grid Thegrid, int row, Layout buttonGrid)
    {
        Thegrid.Children.Add(buttonGrid);
        Thegrid.SetRow(buttonGrid, row);
    }
    public static void AddAtRow(this Grid Thegrid, int row, HorizontalStackLayout buttonGrid)
    {
        Thegrid.Children.Add(buttonGrid);
        Thegrid.SetRow(buttonGrid, row);
    }
    public static void AddAtRow(this Grid Thegrid, int row, MediaElement buttonGrid)
    {
        Thegrid.Children.Add(buttonGrid);
        Thegrid.SetRow(buttonGrid, row);
    }
}
