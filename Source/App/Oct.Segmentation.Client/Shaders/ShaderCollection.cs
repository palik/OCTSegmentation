namespace Oct.Segmentation.Client.Shaders
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Graphics;

    using Microsoft.Xna.Framework.Graphics;

    public static class ShaderCollection
    {
        static readonly GraphicsDevice resourceDevice = GraphicsDeviceManager.Current.GraphicsDevice;

        public static VertexShader DefaultVertexShader;

        public static PixelShader TransparentSlicePixelShader;

        public static PixelShader SelectedSlicePixelShader;

        static ShaderCollection()
        {
            Stream shaderStream = Application.GetResourceStream(new Uri(@"Oct.Segmentation.Client;component/Shaders/Image.vs", UriKind.Relative)).Stream;
            DefaultVertexShader = VertexShader.FromStream(resourceDevice, shaderStream);

            shaderStream = Application.GetResourceStream(new Uri(@"Oct.Segmentation.Client;component/Shaders/Image.ps", UriKind.Relative)).Stream;
            TransparentSlicePixelShader = PixelShader.FromStream(resourceDevice, shaderStream);

            shaderStream = Application.GetResourceStream(new Uri(@"Oct.Segmentation.Client;component/Shaders/SelectedImage.ps", UriKind.Relative)).Stream;
            SelectedSlicePixelShader = PixelShader.FromStream(resourceDevice, shaderStream);
        }    
    }
}