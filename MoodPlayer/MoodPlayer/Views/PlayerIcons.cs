using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoodPlayer.Views
{
    public class PlayerIcons : INotifyPropertyChanged
    {
        private string _ShuffleIcon { get; set; }
        private string _RepeatIcon { get; set; }
        private string _PlayIcon { get; set; }

        public string ShuffleIcon
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
        public string RepeatIcon
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
        public string PlayIcon
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
