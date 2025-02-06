using Microsoft.Maui.Graphics.Platform;
using PaulSchlyter;
using System.Reflection;
using System.IO;
using Microsoft.Maui.Graphics;

namespace CommunityToolkit.Maui.Sample;

public partial class MediaElementPage : BasePage
{

    private void LoadOverlays()
    {
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly!.GetManifestResourceStream("CommunityToolkit.Maui.Sample.Resources.Images.frame_264_ROI.png")!)
        {
            OverlayImage = PlatformImage.FromStream(stream);
        }
        GraphicsDrawable drawable = new GraphicsDrawable(OverlayImage!, Calculator.sunriseTable!);

        const int OverlayWidth = 852;
        const int OverlayHeight = 227;

        var bitmapExportContext = new PlatformBitmapExportContext(OverlayWidth, OverlayHeight, 1.0f);
        var canvas = bitmapExportContext.Canvas;

        // Draw on the canvas
        drawable.Draw(canvas, new RectF(0, 0, OverlayWidth, OverlayHeight));

        // Export the bitmap to an IImage object
        SavedImage = bitmapExportContext.Image;

    }

    public static IImage SaveCanvasToImage(IDrawable drawable, float width, float height)
    {
        var bitmapExportContext = new PlatformBitmapExportContext((int)width, (int)height, 1.0f);
        var canvas = bitmapExportContext.Canvas;

        // Draw on the canvas
        drawable.Draw(canvas, new RectF(0, 0, width, height));

        // Export the bitmap to an IImage object
        return bitmapExportContext.Image;
    }
}