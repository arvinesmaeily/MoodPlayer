using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoodPlayer.StateManager
{
    public class StateInfo : INotifyPropertyChanged
    {
        private string _State;
        private double _Percentage;

        public string State
        {
            get
            {
                return _State != null ? _State : "";
            }

            set
            {
                if (value != _State)
                {
                    _State = value;
                    OnPropertyChanged("State");
                }
            }
        }

        public double Percentage
        {
            get { return _Percentage; }

            set
            {
                if (value != _Percentage)
                {
                    _Percentage = value;
                    OnPropertyChanged("Percentage");
                }
            }
        }

        #region ShallowCopy     
        [MTAThread]
        public StateInfo ShallowCopy()
        {
            return (StateInfo)MemberwiseClone();
        }
        #endregion

        #region INotifyPropertyChanged Members

        // INotifyPropertyChanged implementation
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