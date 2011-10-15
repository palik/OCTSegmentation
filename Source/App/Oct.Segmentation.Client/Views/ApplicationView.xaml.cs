namespace Oct.Segmentation.Client.Views
{
    using System.Windows.Controls;
    using System.Windows.Graphics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Oct.Segmentation.Client.ViewModels;

    public partial class ApplicationView : UserControl
    {
        public ApplicationView()
        {
            InitializeComponent();
            Loaded += (s, e) => this.ApplicationViewModel = this.DataContext as ApplicationViewModel;
        }

        public ApplicationViewModel ApplicationViewModel { get; private set; }

        private void OnDraw(object sender, DrawEventArgs args)
        {
            var graphicsDevice = GraphicsDeviceManager.Current.GraphicsDevice;

            graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            graphicsDevice.RasterizerState = RasterizerState.CullNone;
            graphicsDevice.BlendState = BlendState.AlphaBlend;

            graphicsDevice.DepthStencilState = DepthStencilState.Default;

            if (this.ApplicationViewModel.Image != null)
            {
                this.ApplicationViewModel.Image.Draw(GraphicsDeviceManager.Current.GraphicsDevice);
            }

            args.InvalidateSurface();
        }
    }
}
