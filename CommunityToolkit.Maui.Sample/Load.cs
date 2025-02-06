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