namespace Oct.Segmentation.Client.Models
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Graphics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Image
    {
        static readonly GraphicsDevice resourceDevice = GraphicsDeviceManager.Current.GraphicsDevice;

        private readonly VertexBuffer vertexBuffer;
        private readonly VertexShader vertexShader;
        private readonly PixelShader pixelShader;
        private readonly float zIndex = -1f;

        public Image(string imagePath, int zIndex)
        {
            this.FilePath = imagePath;
            this.zIndex = zIndex * 0.01f;

            vertexBuffer = this.CreateRectangle();

            Stream shaderStream = Application.GetResourceStream(new Uri(@"Oct.Segmentation.Client;component/Shaders/Image.vs", UriKind.Relative)).Stream;
            vertexShader = VertexShader.FromStream(resourceDevice, shaderStream);

            shaderStream = Application.GetResourceStream(new Uri(@"Oct.Segmentation.Client;component/Shaders/Image.ps", UriKind.Relative)).Stream;
            pixelShader = PixelShader.FromStream(resourceDevice, shaderStream);

            Texture = ContentManager.LoadBitmapFromFile(imagePath);
        }

        VertexBuffer CreateRectangle()
        {
            // create vertices
            var vertices = new VertexPositionTexture[4];

            vertices[0].Position = new Vector3(-1, -1, this.zIndex); // left bottom
            vertices[1].Position = new Vector3(-1, 1, this.zIndex);   // left top 
            vertices[2].Position = new Vector3(1, -1, this.zIndex);  // right bottom
            vertices[3].Position = new Vector3(1, 1, this.zIndex);  // right top
            vertices[0].TextureCoordinate = new Vector2(1, 1);
            vertices[1].TextureCoordinate = new Vector2(1, 0);
            vertices[2].TextureCoordinate = new Vector2(0, 1);
            vertices[3].TextureCoordinate = new Vector2(0, 0);

            // create graphics device managed buffer
            var vb = new VertexBuffer(resourceDevice, VertexPositionTexture.VertexDeclaration,
                vertices.Length, BufferUsage.WriteOnly);

            // copy vertex data to graphics device buffer
            vb.SetData(0, vertices, 0, vertices.Length, 0);

            return vb;
        }

        public string FilePath { get; set; }

        public Texture Texture { get; set; }

        public byte[] RawPixels { get; set; }

        public void Draw(GraphicsDevice graphicsDevice, Matrix worldViewProjection)
        {
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.SetVertexShader(vertexShader);
            graphicsDevice.SetVertexShaderConstantFloat4(0, ref worldViewProjection);

            graphicsDevice.SetPixelShader(pixelShader);

            graphicsDevice.SamplerStates[0] = SamplerState.AnisotropicClamp;
            graphicsDevice.Textures[0] = Texture;

            graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }
    }
}