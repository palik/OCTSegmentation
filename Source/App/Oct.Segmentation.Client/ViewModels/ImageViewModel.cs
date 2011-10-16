namespace Oct.Segmentation.Client.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reactive.Linq;

    using Caliburn.Micro;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Oct.Segmentation.Client.Models;

    public class ImageViewModel : PropertyChangedBase
    {
        private Image2D selectedSlice;

        private Image3D model;

        private Matrix world;
        private Matrix view;
        private Matrix projection;

        private Vector3 cameraPosition;
        private Vector3 cameraTarget;

        public ImageViewModel(string workingDirectory)
        {
            this.Initialize3D();
            this.CreateModel(workingDirectory);
        }

        public Image3D Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
                NotifyOfPropertyChange(() => this.Model);
            }
        }

        public Image2D SelectedSlice
        {
            get
            {
                if (this.selectedSlice == null)
                {
                    this.SelectedSlice = this.Model.Slices.FirstOrDefault();
                }

                return this.selectedSlice;
            }
            set
            {
                if (value != null)
                {
                    this.Model.Slices.Apply(x => x.IsSelected = false);
                    this.selectedSlice = value;
                    this.selectedSlice.IsSelected = true;
                    NotifyOfPropertyChange(() => SelectedSlice);
                }
            }
        }

        public float RotationYAngle { get; set; }

        public float RotationXAngle { get; set; }

        public float Zoom { get; set; }

        private void CreateModel(string workingDirectory)
        {
            var images = new List<Image2D>();

            IObservable<Image2D> observable = this.LoadImages(workingDirectory).ToObservable();
            observable.Subscribe(images.Add, () => this.Model = new Image3D(workingDirectory, images));
        }

        private IEnumerable<Image2D> LoadImages(string directory)
        {
            var files = Directory.EnumerateFiles(directory, "*.jpg");

            var numberOfFiles = files.Count();

            var index = 0;
            foreach (var file in files)
            {
                yield return new Image2D(Path.Combine(directory, file), index - numberOfFiles/2);
                index++;
            }
        }

        private void Initialize3D()
        {
            cameraPosition = new Vector3(0, 0, 5.0f);
            cameraTarget = Vector3.Zero;
        }

        public void Draw(GraphicsDevice device, float aspectRatio)
        {
            cameraPosition.Z += this.Zoom;

            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            world = Matrix.CreateRotationY(this.RotationYAngle) * Matrix.CreateRotationX(this.RotationXAngle);

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 100.0f);

            Array.Sort(
                this.Model.Slices, delegate(Image2D slice1, Image2D slice2)
                    {
                        var s1Position = slice1.GetXZWorldPosition(world);
                        var s2Position = slice2.GetXZWorldPosition(world);

                        var s1DistanceFromCamera = Vector2.Distance(new Vector2(0, 5), s1Position);
                        var s2DistanceFromCamera = Vector2.Distance(new Vector2(0, 5), s2Position);

                        return -(s1DistanceFromCamera.CompareTo(s2DistanceFromCamera));
                    });

            foreach (var slice in this.Model.Slices)
            {
                slice.Draw(device, world * view * projection);
            }
        }

        public void SetAspectRatio(float aspectRatio)
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1.0f, 100.0f);
        }
    }
}