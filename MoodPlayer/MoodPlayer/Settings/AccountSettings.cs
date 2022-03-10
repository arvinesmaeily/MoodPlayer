using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoodPlayer.Settings
{
    public class AccountSettings : INotifyPropertyChanged
    {
        private ISettings Settings = CrossSettings.Current;
        private bool _ClientAuthorized
        {
            get => Settings.GetValueOrDefault(nameof(_ClientAuthorized), false);
            set => Settings.AddOrUpdateValue(nameof(_ClientAuthorized), value);
        }
        private bool _SpotifyAuthorized
        {
            get => Settings.GetValueOrDefault(nameof(_SpotifyAuthorized), false);
            set => Settings.AddOrUpdateValue(nameof(_SpotifyAuthorized), value);
        }
        private string _ClientToken
        {
            get => Settings.GetValueOrDefault(nameof(_ClientToken), "");
            set => Settings.AddOrUpdateValue(nameof(_ClientToken), value);
        }
        private string _SpotifyToken
        {
            get => Settings.GetValueOrDefault(nameof(_SpotifyToken), "");
            set => Settings.AddOrUpdateValue(nameof(_SpotifyToken), value);
        }

        public bool ClientAuthorized
        {
            get { return this._ClientAuthorized; }

            set
            {
                if (value != this._ClientAuthorized)
                {
                    this._ClientAuthorized = value;
                    OnPropertyChanged("ClientAuthorized");
                }
            }
        }
        public bool SpotifyAuthorized
        {
            get { return this._SpotifyAuthorized; }

            set
            {
                if (value != this._SpotifyAuthorized)
                {
                    this._SpotifyAuthorized = value;
                    OnPropertyChanged("SpotifyAuthorized");
                }
            }
        }
        public string ClientToken
        {
            get { return this._ClientToken; }

            set
            {
                if (value != this._ClientToken)
                {
                    this._ClientToken = value;
                    OnPropertyChanged("ClientToken");
                }
            }
        }
        public string SpotifyToken
        {
            get { return this._SpotifyToken; }

            set
            {
                if (value != this._SpotifyToken)
                {
                    this._SpotifyToken = value;
                    OnPropertyChanged("SpotifyToken");
                }
            }
        }

        [MTAThread]
        public AccountSettings ShallowCopy()
        {
            return (AccountSettings)this.MemberwiseClone();
        }

        public void Clear()
        {
            ClientAuthorized = false;
            SpotifyAuthorized = false;
            ClientToken = "";
            SpotifyToken = "";
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
