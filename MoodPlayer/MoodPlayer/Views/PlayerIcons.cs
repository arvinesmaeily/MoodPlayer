using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MoodPlayer.Views
{
    public class PlayerIcons : INotifyPropertyChanged
    {
        private UriImageSource _ShuffleIcon { get; set; }
        private UriImageSource _RepeatIcon { get; set; }
        private UriImageSource _PlayIcon { get; set; }

        public UriImageSource ShuffleIcon
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
        public UriImageSource RepeatIcon
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
        public UriImageSource PlayIcon
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
        [MTAThread]
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        [MTAThread]
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
