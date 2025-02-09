using Microsoft.Maui.Graphics.Platform;
using PaulSchlyter;
using System.Reflection;
using System.IO;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{
    GraphicsView? OverlayViewRise;
    GraphicsView? OverlayViewSet;


    private static IImage? OverlayImage;
    private static IImage? SavedRiseImage;
    private static IImage? SavedSetImage;
    public class GraphicsDrawable(IImage image, string text) : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Draw the image
            canvas.DrawImage(image, 0, 0, dirtyRect.Width, dirtyRect.Height);

            // Draw the text
            // x 130 y 286 of 852 x 480
            const int fontSize = 24;
            const float videoXfraction = 130.0f/852;
            float x1 = videoXfraction*dirtyRect.Width; // x1 seems ok!

            canvas.Font = new Font("monospace");
            canvas.FontSize = fontSize;
            canvas.FontColor = Colors.White;
         
            string[] ss = text.Split('\n');
            float y = 10;
            foreach (string s in ss)
            {
                canvas.DrawString(s, x1, y, dirtyRect.Width - x1, dirtyRect.Height, HorizontalAlignment.Left, VerticalAlignment.Top);
                y += fontSize - 8;
            }
        }
    }
    public class GraphicsDrawable2 : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            const float videoYfraction = 248.0f/480;
            float y1 = videoYfraction*dirtyRect.Bottom;
            canvas.DrawImage(SavedRiseImage, 0, y1, dirtyRect.Width, dirtyRect.Bottom-y1);  // !! OK !! //Drawing image at position (0, 77,54921) with width 411,4286 and height 150,0952
        }
    }
    public class GraphicsDrawable3 : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            const float videoYfraction = 248.0f/480;
            float y1 = videoYfraction*dirtyRect.Bottom;
            canvas.DrawImage(SavedSetImage, 0, 0, dirtyRect.Width, dirtyRect.Bottom-y1);  //
        }
    }
    private void LoadOverlays()
    {
        LoadRise();
        LoadSet();
    }

    private void LoadRise()
    {
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly!.GetManifestResourceStream("CommunityToolkit.Maui.Sample.Resources.Images.frame_264_ROI.png")!)
        {
            OverlayImage = PlatformImage.FromStream(stream);
        }
        GraphicsDrawable drawable = new(OverlayImage!, Calculator.sunriseTable!);

        const int OverlayWidthPixels = 852;
        const int OverlayHeightPixels = 227;

        PlatformBitmapExportContext bitmapExportContext = new(OverlayWidthPixels, OverlayHeightPixels, 1.0f);
        ICanvas canvas = bitmapExportContext.Canvas;

        // Draw on the canvas
        drawable.Draw(canvas, new RectF(0, 0, OverlayWidthPixels, OverlayHeightPixels));

        // Export the bitmap to an IImage object
        SavedRiseImage = bitmapExportContext.Image;

        OverlayViewRise = new()
        {
            AnchorX = 0,
            AnchorY = 0,
            TranslationX = 0,
            TranslationY = 0,
            Drawable = new GraphicsDrawable2()
        };
    }
    private void LoadSet()
    {
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly!.GetManifestResourceStream("CommunityToolkit.Maui.Sample.Resources.Images.frame_969_ROI.png")!)
        {
            OverlayImage = PlatformImage.FromStream(stream);
        }
        GraphicsDrawable drawable = new(OverlayImage!, Calculator.sunsetTable!);

        const int OverlayWidthPixels = 852;
        const int OverlayHeightPixels = 230;

        PlatformBitmapExportContext bitmapExportContext = new(OverlayWidthPixels, OverlayHeightPixels, 1.0f);
        ICanvas canvas = bitmapExportContext.Canvas;

        // Draw on the canvas
        drawable.Draw(canvas, new RectF(0, 0, OverlayWidthPixels, OverlayHeightPixels));

        // Export the bitmap to an IImage object
        SavedSetImage = bitmapExportContext.Image;

        OverlayViewSet = new()
        {
            AnchorX = 0,
            AnchorY = 0,
            TranslationX = 0,
            TranslationY = 0,
            Drawable = new GraphicsDrawable3()
        };
    }
}