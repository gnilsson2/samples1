﻿#define Medium
#undef Medium
#define pixel_7

using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using PaulSchlyter;
using System;
using System.ComponentModel;

namespace CommunityToolkit.Maui.Sample;
public partial class MediaElementPage : BasePage
{
    private readonly MediaElement MediaElement;
    public MediaElementPage(BaseViewModel viewModel)
    {
        BindingContext = viewModel;
        //Padding = 12;

        //var today = new DateTime(2024, 1, 9);
        Calculate(DateTime.Now);

        MediaElement = new MediaElement
        {
            ShouldAutoPlay = true,
            Source = MediaSource.FromResource("kort1.mp4")
        };

        BuildGrid();

        MediaElement!.PropertyChanged += MediaElement_PropertyChanged;
    }
    //TODO: kort2.mp4 with hardcoded positions
    //TODO: solfilmen0_utkast.mp4, timing.
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

        Grid x = AddFilmen();
        Thegrid.AddAtRow(0, x);


        Thegrid.AddAtRow(1, AddState());

        Thegrid.AddAtRow(2, AddButtons());

        Thegrid.AddAtRow(3, AddPosition());

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

        var image = new Microsoft.Maui.Controls.Image { Source = "overlay_image.png" };


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

        AddRiseVertical(image, grid);
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
            Text = sunriseTable,
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
#elif  pixel_7
        image.TranslationX = 40;
        image.TranslationY = 74;

#else //7a
        image.TranslationX = 40;
        image.TranslationY = 72;
#endif
        grid.Add(image);

        var label = new Label
        {
            Text = sunriseTable,
            TextColor = Color.FromRgba(255, 255, 255, 128),
            AnchorX = 0,
            AnchorY = 0,
#if Medium
            FontSize = 13,
            TranslationX = 150,
            TranslationY = 174,
#elif pixel_7
            FontSize = 13,
            TranslationX = 150,
            TranslationY = 176,
#else
            FontSize = 11,
            TranslationX = 130,
            TranslationY = 174,
#endif
        }; 
        grid.Add(label);
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


    private static string Format_time(DateTime dateTime)
    {
        return $"  {dateTime.AddSeconds(30):H:mm}   ";
    }

    private static string Format_time_difference(TimeSpan result)
    {
        var fmt = (result < TimeSpan.Zero ? "\\-" : "\\+") + "h\\:mm";
        var diff = result.ToString(fmt);
        return diff + "   ";
    }
    private const string NAtime = "    -    ";

    class Coordinate(double latitude, double longitude)
    {
        public double Latitude { get; set; } = latitude;
        public double Longitude { get; set; } = longitude;
    }
    class Place
    {
        public required string Name { get; set; }
        public required Coordinate Coordinate { get; set; }
    }

    static readonly Place[] places =
    [
        new Place { Name = "Lund", Coordinate = new Coordinate(55.708333, 13.199167) },
        new Place { Name = "Stockholm", Coordinate = new Coordinate(59.329444, 18.068611) },
        new Place { Name = "Lycksele", Coordinate = new Coordinate(64.596389, 18.675278) },
        new Place { Name = "Kiruna", Coordinate = new Coordinate(67.848889, 20.302778) }
    ];

    static string? sunriseTable;
    static string? sunsetTable;

    public static void Calculate(DateTime today)
    {
        object[,,] objects = new object[2, 3, places.Length];

        for (int p = 0; p < places.Length; p++)
        {
#pragma warning disable IDE0042 // Deconstruct variable declaration
            var s1 = Calculator.Get(places[p].Coordinate.Latitude, places[p].Coordinate.Longitude, today);
            var s2 = Calculator.Get(places[p].Coordinate.Latitude, places[p].Coordinate.Longitude, today.AddDays(-7));
#pragma warning restore IDE0042 // Deconstruct variable declaration

            s2.rise = s2.rise.AddDays(7);
            s2.set = s2.set.AddDays(7);

            objects[0, 0, p] = s1.result == DiurnalResult.NormalDay ? Format_time(s1.rise) : NAtime;
            objects[1, 0, p] = s1.result == DiurnalResult.NormalDay ? Format_time(s1.set) : NAtime;
            objects[0, 1, p] = s2.result == DiurnalResult.NormalDay ? Format_time(s2.rise) : NAtime;
            objects[1, 1, p] = s2.result == DiurnalResult.NormalDay ? Format_time(s2.set) : NAtime;
            if (s1.result == DiurnalResult.NormalDay && s2.result == DiurnalResult.NormalDay)
            {
                objects[0, 2, p] = Format_time_difference(s2.rise - s1.rise);
                objects[1, 2, p] = Format_time_difference(s1.set - s2.set);
            }
            else
            {
                objects[0, 2, p] = NAtime;
                objects[1, 2, p] = NAtime;
            }
        }

        sunriseTable = "";
        for (int j = 0; j < 3; j++)
        {
            for (int p = 0; p < places.Length; p++)
            {
                sunriseTable += (objects[0, j, p]);
                sunriseTable += "    ";
            }
            sunriseTable += "\n";
        }

        sunsetTable = "";
        for (int j = 0; j < 3; j++)
        {
            for (int p = 0; p < places.Length; p++)
            {
                sunsetTable += (objects[1, j, p]);
                sunsetTable += "    ";
            }
            sunsetTable += "\n";
        }

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
