using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoodPlayer.Extensions
{
    public class Captcha : INotifyPropertyChanged
    {
        private string _Term { get; set; }
        public string Term
        {
            get { return _Term; }

            set
            {
                if (value != _Term)
                {
                    _Term = value;
                    OnPropertyChanged("Term");
                }
            }
        }
        public Captcha()
        {
            Generate();
        }

        public void Generate() { Term = Bickers.Twaddle.Core.Twaddle.Word.GenerateWord(); }

        [MTAThread]
        public Captcha ShallowCopy()
        {
            return (Captcha)MemberwiseClone();
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
