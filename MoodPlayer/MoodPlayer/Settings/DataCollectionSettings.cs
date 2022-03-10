using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.ComponentModel;

namespace MoodPlayer.Settings
{
    public class DataCollectionSettings : INotifyPropertyChanged
    {
        private ISettings Settings = CrossSettings.Current;
        private bool _CollectSPSensor

        {
            get => Settings.GetValueOrDefault(nameof(_CollectSPSensor), false);
            set => Settings.AddOrUpdateValue(nameof(_CollectSPSensor), value);
        }
        private bool _CollectCarSensor
        {
            get => Settings.GetValueOrDefault(nameof(_CollectCarSensor), false);
            set => Settings.AddOrUpdateValue(nameof(_CollectCarSensor), value);
        }
        private bool _CollectSpotify
        {
            get => Settings.GetValueOrDefault(nameof(_CollectSpotify), false);
            set => Settings.AddOrUpdateValue(nameof(_CollectSpotify), value);
        }
        private bool _CollectVoice
        {
            get => Settings.GetValueOrDefault(nameof(_CollectVoice), false);
            set => Settings.AddOrUpdateValue(nameof(_CollectVoice), value);
        }


        public bool CollectSPSensor
        {
            get { return _CollectSPSensor; }

            set
            {
                if (value != _CollectSPSensor)
                {
                    _CollectSPSensor = value;
                    OnPropertyChanged("CollectSPSensor");
                }
            }
        }
        public bool CollectCarSensor
        {
            get { return _CollectCarSensor; }

            set
            {
                if (value != _CollectCarSensor)
                {
                    _CollectCarSensor = value;
                    OnPropertyChanged("CollectCarSensor");
                }
            }
        }
        public bool CollectSpotify
        {
            get { return _CollectSpotify; }

            set
            {
                if (value != _CollectSpotify)
                {
                    _CollectSpotify = value;
                    OnPropertyChanged("CollectSpotify");
                }
            }
        }
        public bool CollectVoice
        {
            get { return _CollectVoice; }

            set
            {
                if (value != _CollectVoice)
                {
                    _CollectVoice = value;
                    OnPropertyChanged("CollectVoice");
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
            CollectCarSensor = false;
            CollectSPSensor = false;
            CollectSpotify = false;
            CollectVoice = false;
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
