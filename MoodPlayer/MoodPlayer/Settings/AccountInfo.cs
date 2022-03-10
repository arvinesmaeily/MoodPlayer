using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoodPlayer.Settings
{
    public class AccountInfo : INotifyPropertyChanged
    {
        private ISettings Settings = CrossSettings.Current;
        private string _ID
        {
            get => Settings.GetValueOrDefault(nameof(_ID), "");
            set => Settings.AddOrUpdateValue(nameof(_ID), value);
        }
        private string _LastLogin
        {
            get => Settings.GetValueOrDefault(nameof(_LastLogin), "");
            set => Settings.AddOrUpdateValue(nameof(_LastLogin), value);
        }
        private string _Username
        {
            get => Settings.GetValueOrDefault(nameof(_Username), "");
            set => Settings.AddOrUpdateValue(nameof(_Username), value);
        }
        private string _Email
        {
            get => Settings.GetValueOrDefault(nameof(_Email), "");
            set => Settings.AddOrUpdateValue(nameof(_Email), value);
        }

        public string ID
        {
            get { return this._ID; }

            set
            {
                if (value != this._ID)
                {
                    this._ID = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        public string LastLogin
        {
            get { return this._LastLogin; }

            set
            {
                if (value != this._LastLogin)
                {
                    this._LastLogin = value;
                    OnPropertyChanged("LastLogin");
                }
            }
        }
        public string Username
        {
            get { return this._Username; }

            set
            {
                if (value != this._Username)
                {
                    this._Username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
        public string Email
        {
            get { return this._Email; }

            set
            {
                if (value != this._Email)
                {
                    this._Email = value;
                    OnPropertyChanged("Email");
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
            ID = "";
            LastLogin = "";
            Username = "";
            Email = "";
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
