using Microsoft.Maui.Graphics.Platform;
using PaulSchlyter;
using System.Reflection;
using System.IO;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Devices;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{
    private static IImage? OverlayImage;
    private static IImage? SavedImage;
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

            canvas.StrokeColor = Colors.Red; //TODO remove
            canvas.DrawRectangle(0, 0, dirtyRect.Width, dirtyRect.Height); //TODO remove

            // Draw the text
            // x 130 y 286 of 852 x 480
            const float videoXfraction = 130.0f/852;
            float x1 = videoXfraction*dirtyRect.Width;

            canvas.Font = new Font("monospace");
            canvas.FontSize = 24;
            canvas.FontColor = Colors.White;
            canvas.DrawString(_text, x1, 0, dirtyRect.Width-x1, dirtyRect.Height, HorizontalAlignment.Left, VerticalAlignment.Top); // X1 seems ok!
        }
    }

    private void LoadOverlays()
    {
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly!.GetManifestResourceStream("CommunityToolkit.Maui.Sample.Resources.Images.frame_264_ROI.png")!)
        {
            OverlayImage = PlatformImage.FromStream(stream);
        }
        GraphicsDrawable drawable = new GraphicsDrawable(OverlayImage!, Calculator.sunriseTable!);

        const int OverlayWidthPixels = 852;
        const int OverlayHeightPixels = 227;

        var bitmapExportContext = new PlatformBitmapExportContext(OverlayWidthPixels, OverlayHeightPixels, 1.0f);
        //var bitmapExportContext = new PlatformBitmapExportContext((int)(OverlayWidthPixels/DeviceDisplay.Current.MainDisplayInfo.Density), (int)(OverlayHeightPixels / DeviceDisplay.Current.MainDisplayInfo.Density), 1.0f);
        var canvas = bitmapExportContext.Canvas;

        // Draw on the canvas
        drawable.Draw(canvas, new RectF(0, 0, OverlayWidthPixels, OverlayHeightPixels));
        //drawable.Draw(canvas, new RectF(0, 0, (float)(OverlayWidthPixels/DeviceDisplay.Current.MainDisplayInfo.Density), (float)(OverlayHeightPixels / DeviceDisplay.Current.MainDisplayInfo.Density)));

        // Export the bitmap to an IImage object
        SavedImage = bitmapExportContext.Image;

    }
}