namespace Oct.Segmentation.Client
{
    using System;
    using System.IO;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Graphics;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Resources;

    using Microsoft.Xna.Framework.Graphics;

    public class ContentManager
    {
        static public Texture2D LoadBitmapFromApplicationResources(string imageName)
        {
            var sr = Application.GetResourceStream(new Uri(imageName, UriKind.Relative));
            var bs = new BitmapImage();
            bs.SetSource(sr.Stream);

            var texture2D = new Texture2D(GraphicsDeviceManager.Current.GraphicsDevice, bs.PixelWidth, bs.PixelHeight, false, SurfaceFormat.Color);
            bs.CopyTo(texture2D);

            return texture2D;
        }

        public static BitmapImage LoadBitmapFromFile(string imagePath)
        {
            var sr = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            var bs = new BitmapImage();
            bs.SetSource(sr);

            return bs;
        }

        static public Texture2D ConvertBitmapToTexture(BitmapImage bs)
        {
            var texture2D = new Texture2D(GraphicsDeviceManager.Current.GraphicsDevice, bs.PixelWidth, bs.PixelHeight, false, SurfaceFormat.Color);
            bs.CopyTo(texture2D);

            return texture2D;
        }
    }
}