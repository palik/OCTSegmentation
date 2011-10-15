namespace Oct.Segmentation.Client.ViewModels
{
    using System.Windows.Controls;

    using Caliburn.Micro;

    public class ApplicationViewModel : PropertyChangedBase
    {
        private bool isBusy = false;

        private ImageViewModel image;

        public ApplicationViewModel()
        {

        }

        public ImageViewModel Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public void ChooseWorkingDirectory()
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == true)
            {
                this.Image = new ImageViewModel(openDialog.File.DirectoryName);
            }
        }
    }
}