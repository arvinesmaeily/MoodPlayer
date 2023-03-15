using System.ComponentModel;

namespace RecommenderSystem
{
    public class RecommenderState : INotifyPropertyChanged
    {

        public RecommenderState()
        {
            _SessionId = 0;
            _StartSequence = 0;
            _EndSequence = 0;
            _RecommendationId = 0;
            _RecommendedMusic = -1;
        }

        private int _SessionId = 0;
        private int _StartSequence = 0;
        private int _EndSequence = 0;
        private int _RecommendationId = 0;
        private int _RecommendedMusic = -1;

        public int SessionId
        {
            get
            {
                return _SessionId;
            }

            set
            {
                if (value != _SessionId)
                {
                    _SessionId = value;
                    OnPropertyChanged("SessionId");
                }
            }
        }

        public int FirstSequence
        {
            get
            {
                return _StartSequence;
            }

            set
            {
                if (value != _StartSequence)
                {
                    _StartSequence = value;
                    OnPropertyChanged("StartSequence");
                }
            }
        }

        public int LastSequence
        {
            get
            {
                return _EndSequence;
            }

            set
            {
                if (value != _EndSequence)
                {
                    _EndSequence = value;
                    OnPropertyChanged("EndSequence");
                }
            }
        }

        public int RecommendationId
        {
            get
            {
                return _RecommendationId;
            }

            set
            {
                if (value != _RecommendationId)
                {
                    _RecommendationId = value;
                    OnPropertyChanged("RecommendationId");
                }
            }
        }

        public int RecommendedMusic
        {
            get
            {
                return _RecommendedMusic;
            }

            set
            {
                if (value != _RecommendedMusic)
                {
                    _RecommendedMusic = value;
                    if(value != -1)
                        OnPropertyChanged("RecommendedMusic");
                }
            }
        }


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
    }
}
