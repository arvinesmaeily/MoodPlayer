using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.ComponentModel;

namespace MoodPlayer.Settings
{
    public class GeneralSettings : INotifyPropertyChanged
    {
        private ISettings Settings = CrossSettings.Current;

        private bool _FirstRun
        {
            get => Settings.GetValueOrDefault(nameof(_FirstRun), true);
            set => Settings.AddOrUpdateValue(nameof(_FirstRun), value);
        }
        private string _DeviceID
        {
            get => Settings.GetValueOrDefault(nameof(_DeviceID), "");
            set => Settings.AddOrUpdateValue(nameof(_DeviceID), value);
        }
        public bool FirstRun
        {
            get { return _FirstRun; }

            set
            {
                if (value != _FirstRun)
                {
                    _FirstRun = value;
                    OnPropertyChanged("FirstRun");
                }
            }
        }
        public string DeviceID
        {
            get { return _DeviceID; }

            set
            {
                if (value != _DeviceID)
                {
                    _DeviceID = value;
                    OnPropertyChanged("DeviceID");
                }
            }
        }

        [MTAThread]
        public GeneralSettings ShallowCopy()
        {
            return (GeneralSettings)MemberwiseClone();
        }

        public void Clear()
        {
            FirstRun = true;
            DeviceID = "";
        }

        // INotifyPropertyChanged implementation
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
    }
}
