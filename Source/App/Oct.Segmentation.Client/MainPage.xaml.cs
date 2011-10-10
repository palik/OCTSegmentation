using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Oct.Segmentation.Client
{
    using System.Windows.Graphics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Oct.Segmentation.Client.ViewModels;

    public partial class MainPage : UserControl
    {
        public MainPage()
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
            graphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
            //graphicsDevice.BlendState = new BlendState()
            //    {
            //        AlphaSourceBlend = Blend.SourceAlpha,
            //        ColorSourceBlend = Blend.SourceAlpha
            //    };

            this.ApplicationViewModel.DrawImage3D();

            args.InvalidateSurface();
        }
    }
}
