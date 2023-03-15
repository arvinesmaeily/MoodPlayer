using DataCollectionManager.CustomTimer;
using DataCollectionManager.MasterDataManager;
using DataCollectionManager.Music.MusicDataUtils;
using MusicPlayer;
using MusicPlayer.MusicUtil;
using RecommenderSystem;
using SettingsManager;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MusicPlayer.Player;

namespace MoodPlayer.Views
{
    public partial class PlayerPage : ContentPage
    {

        private PlayerIcons PlayerIcons = new PlayerIcons();
        private PercisionTimer PercisionTimer = new PercisionTimer(new TimeSpan(0, 0, 0, 1, 0));

        private bool Continue = false;

        public PlayerPage()
        {
            InitializeComponent();
            //buttonShuffle.BindingContext = this.PlayerIcons;
            buttonPlay.BindingContext = this.PlayerIcons;
            //buttonRepeat.BindingContext = this.PlayerIcons;
            PercisionTimer.ThresholdReachedMilliSecond += PercisionTimer_ThresholdReachedMilliSecond;
            
            LabelStatus.BindingContext = DataRecordingManager.Status;
            labelRemaining.BindingContext = DataRecordingManager.Status;

            Player.MediaQueue.CurrentItemChanged += MediaQueue_CurrentItemChanged;

            InitializeButtons();
            PercisionTimer.TurnOn();

            Recommender.RecommenderState.PropertyChanged += RecommenderState_PropertyChanged;
            Player.MediaPlayer.PlaybackEnded += MediaPlayer_PlaybackEnded;
            
            Library.Load();
            Library.SetPlaylist(Library.Data);
        }

        private void MediaPlayer_PlaybackEnded(object sender, EventArgs e)
        {
            if (Continue)
            {
                Recommender.RecommenderState.SessionId = DataRecordingManager.SessionId;
                Recommender.RecommenderState.FirstSequence = Recommender.RecommenderState.LastSequence;
                Recommender.RecommenderState.LastSequence = DataRecordingManager.SequenceId;
                Recommender.GetRecommendation();
            }
        }

        private void RecommenderState_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "RecommendedMusic" && Continue)
            {

                Player.RecommendPlay(Recommender.RecommenderState.RecommendedMusic);
            }
        }

        private void PercisionTimer_ThresholdReachedMilliSecond(object sender, TimerThresholdReachedEventArgs e)
        {
            Device.BeginInvokeOnMainThread((() =>
            {
                double Duration = MediaPlayer.Duration;
                double Position = MediaPlayer.CurrentPosition;

                labelDuration.Text = TimeSpan.FromSeconds(Duration).ToString(@"mm\:ss");
                labelPosition.Text = TimeSpan.FromSeconds(Position).ToString(@"mm\:ss");
                progressBarPosition.Progress = Position / Duration;

                if (MediaPlayer.IsPlaying)
                {
                    PlayerIcons.PlayIcon = new FileImageSource()
                    {
                        File = "iconpause.png"
                    };
                }
                else
                {
                    PlayerIcons.PlayIcon = new FileImageSource()
                    {
                        File = "iconplay.png"
                    };
                }

            }));
        }

        override protected void OnAppearing()
        {
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Player.MediaQueue.ShuffleModeChanged += MediaQueue_ShuffleModeChanged;
                Player.MediaQueue.RepeatModeChanged += MediaQueue_RepeatModeChanged;
                //Player.PlayerSettings.PropertyChanged += PlayerSettings_PropertyChanged;
                //PlayerSettings_PropertyChanged(null, null);
                RepeatModeChangedEventArgs repeatModeChangedEventArgs = new RepeatModeChangedEventArgs() { RepeatMode = Player.MediaQueue.RepeatMode };
                ShuffleModeChangedEventArgs shuffleModeChangedEventArgs = new ShuffleModeChangedEventArgs() { ShuffleMode = Player.MediaQueue.ShuffleMode };
                MediaQueue_RepeatModeChanged(null, repeatModeChangedEventArgs);
                MediaQueue_ShuffleModeChanged(null, shuffleModeChangedEventArgs);
            });
        }

/*        private void PlayerSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                //playback mode
                if (Player.PlayerSettings.IsPlaying)
                {

                }
                else if (!(Player.PlayerSettings.IsPlaying))
                {

                }
            });
        }*/

        private void MediaQueue_RepeatModeChanged(object sender, RepeatModeChangedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                //repeat settings
                if (e.RepeatMode == RepeatMode.All)
                {
                    Player.PlayerSettings.Repeat = RepeatMode.All.ToString();
                    PlayerIcons.RepeatIcon = new FileImageSource()
                    {
                        File = "iconrepeatall.png"
                    };
                }
                else if (e.RepeatMode == RepeatMode.One)
                {
                    Player.PlayerSettings.Repeat = RepeatMode.One.ToString();
                    PlayerIcons.RepeatIcon = new FileImageSource()
                    {
                        File = "iconrepeatone.png"
                    };
                }
                else if (e.RepeatMode == RepeatMode.Off)
                {
                    Player.PlayerSettings.Repeat = RepeatMode.Off.ToString();
                    PlayerIcons.RepeatIcon = new FileImageSource()
                    {
                        File = "iconrepeatoff.png"
                    };
                }
            });
        }

        private void MediaQueue_ShuffleModeChanged(object sender, ShuffleModeChangedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                //shuffle settings
                if (e.ShuffleMode == ShuffleMode.All)
                {
                    Player.PlayerSettings.Shuffle = ShuffleMode.All.ToString();
                    PlayerIcons.ShuffleIcon = new FileImageSource()
                    {
                        File = "iconshuffleon.png"
                    };
                }
                else if (e.ShuffleMode == ShuffleMode.Off)
                {
                    Player.PlayerSettings.Shuffle = ShuffleMode.Off.ToString();
                    PlayerIcons.ShuffleIcon = new FileImageSource()
                    {
                        File = "iconshuffleoff.png"
                    };
                }
            });
        }

        private void MediaQueue_CurrentItemChanged(object sender, MusicPlayer.MusicUtil.CurrentItemChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                labelSongArtist.Text = Player.MediaQueue.CurrentItem.Artist;
                labelSongTitle.Text = Player.MediaQueue.CurrentItem.Title;
                //songImage.Source = ImageSource.FromUri(new Uri(Player.MediaQueue.CurrentItem.ImageUri));
            });
        }

        private void buttonPlay_Clicked(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
            if (!Player.MediaPlayer.IsPlaying)
            {
                    Task.Run(() =>
                    {
                        Continue = true;
                        App.SetRecordTransmit("start");

                        Recommender.RecommenderState.SessionId = DataRecordingManager.SessionId;
                        Recommender.RecommenderState.FirstSequence = 0;
                        Recommender.RecommenderState.LastSequence = 0;
                        Recommender.GetRecommendation();
                    });

                }
                else
                {
                    Continue= false;
                    buttonPlay.IsVisible = false;
                    Player.Stop();
                    App.SetRecordTransmit("stop");

                    Recommender.RecommenderState.SessionId = DataRecordingManager.SessionId;
                    Recommender.RecommenderState.FirstSequence = Recommender.RecommenderState.LastSequence;
                    Recommender.RecommenderState.LastSequence = DataRecordingManager.SequenceId;
                    Recommender.GetRecommendation();
                }
            });
        }

        private void buttonPrev_Clicked(object sender, EventArgs e)
        {
            Player.Previous();
        }

        private void buttonNext_Clicked(object sender, EventArgs e)
        {
            Player.Next();
        }

        private void buttonShuffle_Clicked(object sender, EventArgs e)
        {
            Player.ChangeShuffle();
        }

        private void buttonRepeat_Clicked(object sender, EventArgs e)
        {
            //if (Player.MediaPlayer.Queue.Count > 0)
            {
                Player.ChangeRepeat();
            }
        }

    }

}