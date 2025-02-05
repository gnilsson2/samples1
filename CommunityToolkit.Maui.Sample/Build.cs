//#define p4inch
#define Medium
//#define pixel_7

using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Layouts;
using PaulSchlyter;
using System;
using IImage = Microsoft.Maui.Graphics.IImage;
using Image = Microsoft.Maui.Controls.Image;
using Label = Microsoft.Maui.Controls.Label;
namespace CommunityToolkit.Maui.Sample;
public partial class MediaElementPage : BasePage
{
    //TODO: kort2.mp4 with hardcoded positions

    private void BuildGrid()
    {
        Grid Thegrid;
        Thegrid = new Grid
        {
            RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(150) },
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

        //This dont work, would need DeviceDisplay.Current.MainDisplayInfoChanged += 
        //if (DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
        //    AddRisePortrait(grid);
        //else
        //    AddRiseLandscape(grid);

        AddRise(grid);

        return grid;
    }

    public class GraphicsDrawable : IDrawable
    {
        private readonly IImage _image;
        private readonly string _text;

        public GraphicsDrawable(IImage image, string text)
        {
            _image = image;
            _text = text;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Draw the image
            canvas.DrawImage(_image, 0, 0, dirtyRect.Width, dirtyRect.Height);

            // Draw the text
            canvas.FontSize = 12;
            canvas.FontColor = Colors.Red;
            canvas.DrawString(_text, 0, 0, dirtyRect.Width, dirtyRect.Height, HorizontalAlignment.Left, VerticalAlignment.Top);
        }
    }
    public class GraphicsDrawable2 : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            //Width=411,4286, Height=150,0952 Portait
            //Width=914,2857, Height=150,0952 Landscape
            //const float factorX = 0.357f;
            //const float factorW = 0.504f;
            float y1 = 230.0f/480*dirtyRect.Bottom;
            //canvas.DrawImage(SavedImage, dirtyRect.Width*factorX, 100, dirtyRect.Width*factorW, 48);
            canvas.DrawImage(SavedImage, 0, y1, dirtyRect.Width, dirtyRect.Bottom);
        }
    }
    private void AddRise(Grid grid)
    {
        OverlayView = new();
        OverlayView.Drawable = new GraphicsDrawable2();
        //OverlayView.AnchorX = 0;
        //OverlayView.AnchorY = 0;
        //OverlayView.WidthRequest = 200; // remove
        //OverlayView.HeightRequest = 48;
        //OverlayView.TranslationX = -30; // 145;
        //OverlayView.TranslationY = 51;
        //OverlayView.ScaleX = 3.1;       // 1.4;
        //OverlayView.ScaleY = 1;
        grid.Add(OverlayView);
    }

//    private static void AddRiseHorizontal(Image image, Grid grid)
//    {
//        image.AnchorX = 0;
//        image.AnchorY = 0;
//        image.Aspect = Aspect.AspectFill;
//#if Medium
//        // Grid: 0, 0, 914.2857142857143, 150
//        // MediaEl: 0, 0, 914.2857142857143, 150.0952380952381
//        // Image: 327.04761904761904, 51.04761904761905, 260.1904761904762, 48
//        // Label: 0, 0, 914.2857142857143, 150.0952380952381

//        image.WidthRequest = 460;
//        image.HeightRequest = 48;
//        image.TranslationX = 100;
//        image.TranslationY = 51;
//#elif p4inch
//        // Grid: 0, 0, 533.3333333333334, 150
//        // MediaEl: 0, 0, 533.3333333333334, 150
//        // Image: 136.66666666666669, 51, 260, 48
//        // Label: 0, 0, 533.3333333333334, 150
        
//        image.WidthRequest = 260;
//        image.HeightRequest = 48;
//        image.TranslationX = 50;
//        image.TranslationY = 50;
//#elif pixel_7
//        // Grid: 0, 0, 862.4761904761905, 150
//        // MediaEl: 0, 0, 862.4761904761905, 150.0952380952381
//        // Image: 301.1428571428571, 51.04761904761905, 260.1904761904762, 48
//        // Label: 0, 0, 862.4761904761905, 150.0952380952381

//        image.WidthRequest = 460;
//        image.HeightRequest = 48;
//        image.TranslationX = 100;
//        image.TranslationY = 50;

//#else //7a
//        // Grid:    0, 0, 745.7391304347826, 150
//        // MediaEl: 0, 0, 745.7391304347826, 150.2608695652174
//        // Image:   152.8695652173913, 51.1304347826087, 440, 48
//        // Label:   0, 0, 745.7391304347826, 150.2608695652174

//        image.WidthRequest = 440;
//        image.HeightRequest = 48;
//        image.TranslationX = 110;
//        image.TranslationY = 51;
//#endif
//        grid.Add(image);

//        TheTextVertical = new Label
//        {
//            Text = Calculator.sunriseTable, //Needs more spaces on GridLength(150)
//            TextColor = Color.FromRgba(255, 255, 255, 128),
//            AnchorX = 0,
//            AnchorY = 0,
//            // 1.095 times larger Rendersize on simulators  = 2.875/2.625 Density7a/Densitypixel_7
//            // but 150/130 = 1.154
//#if Medium // 2.625 1080x2400
//            FontSize = 10,
//            TranslationX = 330,
//            TranslationY = 100,
//#elif p4inch //1.5 480x800
//            FontSize = 10, 
//            TranslationX = 190,
//            TranslationY = 100,
//#elif pixel_7 // 2.625 1080x2400
//            FontSize = 10,
//            TranslationX = 310,
//            TranslationY = 100,
//#else //7a //2.875 1080x2400
//            FontSize = 10,
//            TranslationX = 280,
//            TranslationY = 100,
//#endif
//        };

//        grid.Add(TheTextVertical);
//    }


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
        var buttonGrid = new Grid
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
        var infoGrid = new Grid
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
