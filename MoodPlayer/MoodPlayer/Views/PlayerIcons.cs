using System.ComponentModel;
using Xamarin.Forms;

namespace MoodPlayer.Views
{
    public class PlayerIcons : INotifyPropertyChanged
    {
        private FileImageSource _ShuffleIcon { get; set; }
        private FileImageSource _RepeatIcon { get; set; }
        private FileImageSource _PlayIcon { get; set; }

        public FileImageSource ShuffleIcon
        {
            get
            {
                return _ShuffleIcon;
            }

            set
            {
                _ShuffleIcon = value;
                OnPropertyChanged(nameof(ShuffleIcon));
            }
        }
        public FileImageSource RepeatIcon
        {
            get
            {
                return _RepeatIcon;
            }

            set
            {
                _RepeatIcon = value;
                OnPropertyChanged(nameof(RepeatIcon));
            }
        }
        public FileImageSource PlayIcon
        {
            get
            {
                return _PlayIcon;
            }

            set
            {
                _PlayIcon = value;
                OnPropertyChanged(nameof(PlayIcon));
            }
        }


        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
