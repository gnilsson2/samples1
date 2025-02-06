using Microsoft.Maui.Graphics.Platform;
using PaulSchlyter;
using System.Reflection;
using System.IO;
using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{
    private static IImage? OverlayImage;
    private static IImage? SavedImage;
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

    private void LoadOverlays()
    {
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly!.GetManifestResourceStream("CommunityToolkit.Maui.Sample.Resources.Images.frame_264_ROI.png")!)
        {
            OverlayImage = PlatformImage.FromStream(stream);
        }
        GraphicsDrawable drawable = new(OverlayImage!, Calculator.sunriseTable!);

        const int OverlayWidthPixels = 852;
        const int OverlayHeightPixels = 227;

        var bitmapExportContext = new PlatformBitmapExportContext(OverlayWidthPixels, OverlayHeightPixels, 1.0f);
        var canvas = bitmapExportContext.Canvas;

        // Draw on the canvas
        drawable.Draw(canvas, new RectF(0, 0, OverlayWidthPixels, OverlayHeightPixels));

        // Export the bitmap to an IImage object
        SavedImage = bitmapExportContext.Image;

    }
}