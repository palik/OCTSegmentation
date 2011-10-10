namespace Oct.Segmentation.Client.Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Image3D
    {
        private Matrix world;
        private Matrix view;
        private Matrix projection;

        private float rotationAngle = 0.0f;

        public Image3D(string directoryPath)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(directoryPath, "*.jpg");

            int numberOfFiles = files.Count();
            Images = new Image[numberOfFiles];

            int index = 0;
            foreach (var file in files)
            {
                Images[index] = new Image(Path.Combine(directoryPath, file), index);
                index++;
            }

            Vector3 cameraPosition = new Vector3(0, 0, 5.0f); // the camera's position
            Vector3 cameraTarget = Vector3.Zero;

            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1.4f, 1.0f, 100.0f);
        }

        public Image[] Images { get; set; } 

        public void Draw(GraphicsDevice device)
        {
            world = Matrix.CreateRotationY(rotationAngle);

            foreach (var image in Images)
            {
                image.Draw(device, world * view * projection);
            }

            rotationAngle += 0.005f;
        }
    }
}