#define p4inch
//#define Medium
//#define pixel_7

using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using PaulSchlyter;
namespace CommunityToolkit.Maui.Sample;
public partial class MediaElementPage : BasePage
{
    //TODO: kort2.mp4 with hardcoded positions

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

        //var x = DeviceDisplay.Current.MainDisplayInfo.Width;
        //var y = DeviceDisplay.Current.MainDisplayInfo.Height;

        //var screenwidth = DeviceDisplay.Current.MainDisplayInfo.Width;
        //var screenheight = DeviceDisplay.Current.MainDisplayInfo.Height;

        //double scalex(int x){
        //    return (x * screenwidth / 852);
        //}
        //double scaley(int x) {
        //    return (x * screenheight / 480);
        //}



        //AbsoluteLayout.SetLayoutBounds(MediaElement, new Rect(0, 0, 400, 240)); //TODO scale !!!
        //AbsoluteLayout.SetLayoutFlags(MediaElement, AbsoluteLayoutFlags.PositionProportional);

        //AbsoluteLayout.SetLayoutBounds(image, new Rect(0.1, 0.1, 300, 140)); //TODO scale !!!
        //AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);

        //AbsoluteLayout absoluteLayout = new AbsoluteLayout
        //{
        //    Margin = new Thickness(0),
        //    Children =  { MediaElement, image },
        //};

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
        //absoluteLayout.Add(MediaElement, new Point(0, 0));

        AddRiseVertical(OverlayImage, grid);
        //AddSet(image, absoluteLayout);
        //AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.None);

        return grid;
    }
    private static void AddRiseHorizontal(Image image, Grid grid)
    {
        image.AnchorX = 0;
        image.AnchorY = 0;
        image.Aspect = Aspect.AspectFill;
        image.WidthRequest = 200;
        image.HeightRequest = 50;
        image.TranslationX = 40;
        image.TranslationY = 60;

        grid.Add(image);

        var label = new Label
        {
            Text = Calculator.sunriseTable,
            TextColor = Color.FromRgba(255, 255, 255, 128),
            FontSize = 11,
            AnchorX = 0,
            AnchorY = 0,
            TranslationX = 130,
            TranslationY = 160,
        };
        grid.Add(label);
    }
    private static void AddRiseVertical(Image image, Grid grid)
    {
        image.AnchorX = 0;
        image.AnchorY = 0;
        image.Aspect = Aspect.AspectFill;
        image.WidthRequest = 200;
        image.HeightRequest = 50;
#if Medium
        image.TranslationX = 40;
        image.TranslationY = 72;
#elif p4inch
        image.TranslationX = 45;
        image.TranslationY = 74;
#elif  pixel_7
        image.TranslationX = 40;
        image.TranslationY = 74;

#else //7a
        image.TranslationX = 40;
        image.TranslationY = 72;
#endif
        grid.Add(image);

        TheTextVertical = new Label
        {
            Text = Calculator.sunriseTable,
            TextColor = Color.FromRgba(255, 255, 255, 128),
            AnchorX = 0,
            AnchorY = 0,
            // 1.095 times larger Rendersize on simulators  = 2.875/2.625 Density7a/Densitypixel_7
            // but 150/130 = 1.154
#if Medium // 2.625 1080x2400
            FontSize = 13,
            TranslationX = 150,
            TranslationY = 174,
#elif p4inch //1.5 480x800
            FontSize = 12,
            TranslationX = 110,
            TranslationY = 176,
#elif pixel_7 // 2.625 1080x2400
            FontSize = 13,
            TranslationX = 150,
            TranslationY = 176,
#else //7a //2.875 1080x2400
            FontSize = 11,
            TranslationX = 130,
            TranslationY = 174,
#endif
        };

        grid.Add(TheTextVertical);
    }


    private VerticalStackLayout AddInfo()
    {
        var layout = new VerticalStackLayout
        {
            Padding = new Thickness(0, 0, 0, 15),
            //HorizontalOptions = LayoutOptions.Center
        };
        infolabel = new Label
        {
            //HorizontalOptions = LayoutOptions.Center
            Text = ReadDeviceDisplay(),
        };
        layout.Children.Add(infolabel);

        return layout;
    }
    //private static void AddSet(Image image, AbsoluteLayout absoluteLayout)
    //{
    //    absoluteLayout.Add(image, new Rect(140, 54, 205, 60));

    //    var label = new Label
    //    {
    //        Text = sunsetTable,
    //        TextColor = Color.FromRgba(255, 255, 255, 128),
    //        FontSize = 11,
    //    };
    //    absoluteLayout.Add(label, new Rect(145, 62, 205, 110));
    //}
    private Grid AddButtons()
    {
        var buttonGrid = new Microsoft.Maui.Controls.Grid
        {
            Padding = new Thickness(5, 10, 5, 10),
            ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
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
        var infoGrid = new Microsoft.Maui.Controls.Grid
        {
            HorizontalOptions = LayoutOptions.Fill
        };
        infoGrid.Children.Add(positionLabel);
        infoGrid.Children.Add(durationLabel);

        return infoGrid;
    }

    private HorizontalStackLayout AddState()
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
    public static void AddAtRow(this Grid Thegrid, int row, MediaElement buttonGrid)
    {
        Thegrid.Children.Add(buttonGrid);
        Thegrid.SetRow(buttonGrid, row);
    }

    public static void Add(this AbsoluteLayout absoluteLayout, IView view, Rect bounds, AbsoluteLayoutFlags flags = AbsoluteLayoutFlags.None)
    {
        absoluteLayout.Add(view);
        absoluteLayout.SetLayoutBounds(view, bounds);
        absoluteLayout.SetLayoutFlags(view, flags);
    }
    public static void Add(this AbsoluteLayout absoluteLayout, IView view, Point position)
    {
        absoluteLayout.Add(view);
        absoluteLayout.SetLayoutBounds(view, new Rect(position.X, position.Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
    }
}
