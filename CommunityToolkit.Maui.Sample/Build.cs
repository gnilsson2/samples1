using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Label = Microsoft.Maui.Controls.Label;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{

    private void BuildGrid()
    {
        Grid grid = new()
        {
            RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(160) },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                }
        };

        grid.AddAtRow(0, AddFilmen());
        grid.AddAtRow(1, AddState());
        grid.AddAtRow(2, AddButtons());
        grid.AddAtRow(3, AddPosition());
        grid.AddAtRow(4, AddInfo());

        Content = grid;
    }
    private Grid AddFilmen()
    {
        Grid grid = new()
        {
            Margin = new Thickness(0),
        };

        MediaElement.AnchorX = 0;
        MediaElement.AnchorY = 0;
        MediaElement.Aspect = Aspect.Fill;
        MediaElement.TranslationX = 0;
        MediaElement.TranslationY = 0;

        grid.Add(MediaElement);
        grid.Add(OverlayViewRise);
        grid.Add(OverlayViewSet);

        return grid;
    }

    private VerticalStackLayout AddInfo()
    {
        VerticalStackLayout layout = new()
        {
            Padding = new Thickness(0, 0, 0, 15),
        };
        infolabel = new Label
        {
            Text = ReadDeviceDisplay(),
        };
        layout.Children.Add(infolabel);

        return layout;
    }
    private Grid AddButtons()
    {
        Grid buttonGrid = new()
        {
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 0, 0, 0),
            ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
            ColumnSpacing = 20,
            Scale = 0.7
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
        Button b3 = new() { Text = "Hepp", Command = new Command(OnHeppClicked) };
        buttonGrid.Children.Add(b3);
        buttonGrid.SetColumn(b3, 3);

        return buttonGrid;
    }

    private Grid AddPosition()
    {
        Label positionLabel = new()
        {
            HorizontalOptions = LayoutOptions.Start
        };
        positionLabel.SetBinding(Label.TextProperty, new Binding("Position", source: MediaElement));

        Label durationLabel = new()
        {
            HorizontalOptions = LayoutOptions.End,
            HorizontalTextAlignment = TextAlignment.End
        };
        durationLabel.SetBinding(Label.TextProperty, new Binding("Duration", source: MediaElement));
        
        Grid infoGrid = new()
        {
            HorizontalOptions = LayoutOptions.Fill,
            Padding = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 0, 0, 0),
        };
        infoGrid.Children.Add(positionLabel);
        infoGrid.Children.Add(durationLabel);

        return infoGrid;
    }

    private HorizontalStackLayout AddState()
    {
        HorizontalStackLayout horizontalStackLayout = new()
        {
            Padding = new Thickness(0, 0, 0, 0),
            HorizontalOptions = LayoutOptions.Center
        };
        Label label = new()
        {
            HorizontalOptions = LayoutOptions.Center
        };
        MultiBinding multiBinding = new() { StringFormat = "Current State: {0} - Dimensions: {1}x{2}" };
        multiBinding.Bindings.Add(new Binding("CurrentState", source: MediaElement));
        multiBinding.Bindings.Add(new Binding("MediaWidth", source: MediaElement));
        multiBinding.Bindings.Add(new Binding("MediaHeight", source: MediaElement));
        label.SetBinding(Label.TextProperty, multiBinding);
        horizontalStackLayout.Children.Add(label);

        return horizontalStackLayout;
    }
}

public static partial class MyExtensions
{
    public static void AddAtRow(this Grid grid, int row, Layout layout)
    {
        grid.Children.Add(layout);
        grid.SetRow(layout, row);
    }
}
