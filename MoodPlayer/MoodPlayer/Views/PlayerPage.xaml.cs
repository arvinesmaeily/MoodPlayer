using DependencyManager;
using DataCollectionManager.MasterDataManager;
using MediaManager.Playback;
using MediaManager.Player;
using MediaManager.Queue;
using MoodPlayer.Extensions;
using MoodPlayer.Views;
using MusicPlayer;
using MusicPlayer.MusicUtil;
using SettingsManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MoodPlayer.Views
{
    public partial class PlayerPage : ContentPage
    {

        private PlayerIcons PlayerIcons = new PlayerIcons();

        public PlayerPage()
        {
            InitializeComponent();
            buttonShuffle.BindingContext = this.PlayerIcons;
            buttonPlay.BindingContext = this.PlayerIcons;
            buttonRepeat.BindingContext = this.PlayerIcons;

            LabelStatus.BindingContext = DataRecordingManager.Status;

            Player.MediaPlayer.PositionChanged += PlayerPage_PositionChanged;
            Player.MediaQueue.CurrentItemChanged += MediaQueue_CurrentItemChanged;

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
                Player.PlayerSettings.PropertyChanged += PlayerSettings_PropertyChanged;
                PlayerSettings_PropertyChanged(null, null);
                RepeatModeChangedEventArgs repeatModeChangedEventArgs = new RepeatModeChangedEventArgs() { RepeatMode = Player.MediaQueue.RepeatMode };
                ShuffleModeChangedEventArgs shuffleModeChangedEventArgs = new ShuffleModeChangedEventArgs() { ShuffleMode = Player.MediaQueue.ShuffleMode };
                MediaQueue_RepeatModeChanged(null, repeatModeChangedEventArgs);
                MediaQueue_ShuffleModeChanged(null, shuffleModeChangedEventArgs);
            });
        }

        private void PlayerSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                //playback mode
                if (Player.PlayerSettings.IsPlaying)
                {
                    PlayerIcons.PlayIcon = new FileImageSource()
                    {
                        File = "iconpause.png"
                    };
                }
                else if (!(Player.PlayerSettings.IsPlaying))
                {
                    PlayerIcons.PlayIcon = new FileImageSource()
                    {
                        File = "iconplay.png"
                    };
                }
            });
        }

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
                songImage.Source = ImageSource.FromUri(new Uri(Player.MediaQueue.CurrentItem.ImageUri));
                labelDuration.Text = Player.MediaPlayer.Duration.ToString(@"mm\:ss");
            });
        }

        private void PlayerPage_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {

            Device.BeginInvokeOnMainThread((() =>
            {
                labelPosition.Text = e.Position.ToString(@"mm\:ss");
                sliderPosition.Value = (e.Position.TotalSeconds / Player.MediaPlayer.Duration.TotalSeconds);
            }));
        }


        private void buttonPlay_Clicked(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                if (Player.MediaPlayer.State != MediaPlayerState.Playing)
                {
                    Player.Play();
                    App.SetRecordTransmit("start");
                }
                else
                    Player.Pause();
                App.SetRecordTransmit("stop");
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
        private void buttonFavorite_Clicked(object sender, EventArgs e)
        {

        }

        private void buttonRepeat_Clicked(object sender, EventArgs e)
        {
            if (Player.MediaPlayer.Queue.Count > 0)
            {
                Player.ChangeRepeat();
            }
        }

        private void buttonRecord_Clicked(object sender, EventArgs e)
        {
            if (AppSettings.DataCollectionSettings.CollectVoice == true)
            {
                AppSettings.DataCollectionSettings.CollectVoice = false;
            }
            else
            {
                AppSettings.DataCollectionSettings.CollectVoice = true;
            }
            RenderColors();
        }
        
        private void RenderColors()
        {

        }
    }

}