﻿namespace Oct.Segmentation.Client.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Caliburn.Micro;

    public class Image3D : PropertyChangedBase
    {
        private string workingDirectory;
        private Image2D[] slices;
        private Image2D[] originalSlices;

        public Image3D(string workingDirectory, IList<Image2D> images)
        {
            this.WorkingDirectory = workingDirectory;

            this.OriginalSlices = images.ToArray();
            this.Slices = images.ToArray();
        }

        public Image2D[] OriginalSlices
        {
            get
            {
                return this.originalSlices;
            }
            set
            {
                this.originalSlices = value;
                NotifyOfPropertyChange(() => OriginalSlices);
            }
        }
        
        public Image2D[] Slices
        {
            get
            {
                return this.slices;
            }
            set
            {
                this.slices = value;
                NotifyOfPropertyChange(() => Slices);
            }
        }

        public string WorkingDirectory
        {
            get
            {
                return this.workingDirectory;
            }
            set
            {
                this.workingDirectory = value;
                this.NotifyOfPropertyChange(() => WorkingDirectory);
            }
        } 
    }
}