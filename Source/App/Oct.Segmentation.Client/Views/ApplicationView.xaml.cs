namespace Oct.Segmentation.Client.Views
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Graphics;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Oct.Segmentation.Client.ViewModels;

    using Point = System.Windows.Point;

    public partial class ApplicationView : UserControl
    {
        private const float RotationDelta = 0.02f;
        private const float Zoom = 0.07f;
        private Point previousMouseDownPosition;
        private bool isMouseLeftMouseButtonPressed;

        private float aspectRatio = float.NaN;

        public ApplicationView()
        {
            InitializeComponent();
            Loaded += (s, e) => this.ApplicationViewModel = this.DataContext as ApplicationViewModel;
        }

        public ApplicationViewModel ApplicationViewModel { get; private set; }

        private void SurfaceOnDraw(object sender, DrawEventArgs args)
        {
            var graphicsDevice = GraphicsDeviceManager.Current.GraphicsDevice;

            graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            graphicsDevice.RasterizerState = RasterizerState.CullNone;
            graphicsDevice.BlendState = BlendState.AlphaBlend;

            graphicsDevice.DepthStencilState = DepthStencilState.Default;

            if (this.ApplicationViewModel.Image != null)
            {
                this.ApplicationViewModel.Image.Draw(GraphicsDeviceManager.Current.GraphicsDevice, aspectRatio);
            }

            args.InvalidateSurface();
        }

        private void SurfaceOnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.previousMouseDownPosition = e.GetPosition(this.surface);
            isMouseLeftMouseButtonPressed = true;
        }

        private void SurfaceOnMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isMouseLeftMouseButtonPressed = false;
        }

        private void SurfaceOnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isMouseLeftMouseButtonPressed && this.ApplicationViewModel.Image != null)
            {
                var currentMousePosition = e.GetPosition(surface);

                if (this.previousMouseDownPosition.X - currentMousePosition.X > 0)
                {
                    this.ApplicationViewModel.Image.RotationYAngle -= RotationDelta;
                }

                if (this.previousMouseDownPosition.X - currentMousePosition.X < 0)
                {
                    this.ApplicationViewModel.Image.RotationYAngle += RotationDelta;
                }

                if (this.previousMouseDownPosition.Y - currentMousePosition.Y > 0)
                {
                    this.ApplicationViewModel.Image.RotationXAngle -= RotationDelta;
                }

                if (this.previousMouseDownPosition.Y - currentMousePosition.Y < 0)
                {
                    this.ApplicationViewModel.Image.RotationXAngle += RotationDelta;
                }

                previousMouseDownPosition = currentMousePosition;
            }
        }

        private void SurfaceOnMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                this.ApplicationViewModel.Image.Zoom += Zoom;
            }

            if (e.Delta > 0)
            {
                this.ApplicationViewModel.Image.Zoom -= Zoom;
            }
        }

        private void SurfaceOnSizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            this.aspectRatio = this.GetSurfaceAspectRatio();
        }

        private float GetSurfaceAspectRatio()
        {
            return (float)(this.surface.ActualWidth / this.surface.ActualHeight);
        }
    }
}
