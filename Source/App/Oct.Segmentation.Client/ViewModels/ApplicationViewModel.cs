namespace Oct.Segmentation.Client.ViewModels
{
    using System.Windows.Graphics;
    using Oct.Segmentation.Client.Models;

    public class ApplicationViewModel
    {
        public ApplicationViewModel()
        {
            OctImage = new Image3D(@"D:\Documents\inzynierka\DaneOCT\2009-12-10_103640_sig");
        }

        public Image3D OctImage { get; set; }

        public void DrawImage3D()
        {
            OctImage.Draw(GraphicsDeviceManager.Current.GraphicsDevice);
        }
    }
}