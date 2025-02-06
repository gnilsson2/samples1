using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Label = Microsoft.Maui.Controls.Label;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{

    GraphicsView? OverlayView;

    private void BuildGrid()
    {
        Grid Thegrid;
        Thegrid = new Grid
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

        Grid x = AddFilmen();
        Thegrid.AddAtRow(0, x);

        Thegrid.AddAtRow(1, AddState());

        Thegrid.AddAtRow(2, AddButtons());

        Thegrid.AddAtRow(3, AddPosition());

        Thegrid.AddAtRow(4, AddInfo());

        Content = Thegrid;
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

        AddRise(grid);

        return grid;
    }

    public class GraphicsDrawable2 : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            const float videoYfraction = 248.0f/480;
            float y1 = videoYfraction*dirtyRect.Bottom;
            canvas.DrawImage(SavedImage, 0, y1, dirtyRect.Width, dirtyRect.Bottom-y1);  // !! OK !! //Drawing image at position (0, 77,54921) with width 411,4286 and height 150,0952
        }
    }
    private void AddRise(Grid grid)
    {
        OverlayView = new()
        {
            AnchorX = 0,
            AnchorY = 0,
            TranslationX = 0,
            TranslationY = 0,
            Drawable = new GraphicsDrawable2()
        };
        grid.Add(OverlayView);
    }


    private VerticalStackLayout AddInfo()
    {
        var layout = new VerticalStackLayout
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
        var buttonGrid = new Grid
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
        var infoGrid = new Grid
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
        var horizontalStackLayout = new HorizontalStackLayout
        {
            Padding = new Thickness(0, 0, 0, 0),
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
}

public static partial class MyExtensions
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
}
