namespace Oct.Segmentation.Client.Models
{
    using System.Windows.Graphics;
    using System.Windows.Media.Imaging;

    using Caliburn.Micro;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Oct.Segmentation.Client.Infrastructure;
    using Oct.Segmentation.Client.Shaders;

    public class Image2D : PropertyChangedBase, ISelectable
    {
        private bool isSelected;

        private BitmapImage bitmap;

        static readonly GraphicsDevice resourceDevice = GraphicsDeviceManager.Current.GraphicsDevice;

        private readonly VertexBuffer vertexBuffer;
        private readonly float zIndex = -1f;

        public Image2D(string imagePath, int zIndex)
        {
            this.FilePath = imagePath;
            this.zIndex = zIndex * 0.01f;

            this.Bitmap = ContentManager.LoadBitmapFromFile(imagePath);
            this.Texture = ContentManager.ConvertBitmapToTexture(this.Bitmap);

            vertexBuffer = this.CreateSlice();
        }

        public float ZIndex
        {
            get
            {
                return this.zIndex;
            }
        }

        public BitmapImage Bitmap
        {
            get
            {
                return this.bitmap;
            }
            set
            {
                this.bitmap = value;
                NotifyOfPropertyChange(() => Bitmap);
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
            }
        }

        public Vector3 Position { get; private set; }

        VertexBuffer CreateSlice()
        {
            this.Position = new Vector3(0, 0, this.zIndex);

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
            graphicsDevice.SetVertexShader(ShaderCollection.DefaultVertexShader);
            graphicsDevice.SetVertexShaderConstantFloat4(0, ref worldViewProjection);

            if (!this.IsSelected)
            {
                graphicsDevice.SetPixelShader(ShaderCollection.TransparentSlicePixelShader);
            }
            else
            {
                graphicsDevice.SetPixelShader(ShaderCollection.SelectedSlicePixelShader);
            }

            graphicsDevice.SamplerStates[0] = SamplerState.AnisotropicClamp;
            graphicsDevice.Textures[0] = Texture;

            graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }

        public Vector2 GetXZWorldPosition(Matrix world)
        {
            Vector3 result;
            Vector3 xzPosition = this.Position;
            Vector3.Transform(ref xzPosition, ref world, out result);

            return new Vector2(result.X, result.Z);
        }
    }
}