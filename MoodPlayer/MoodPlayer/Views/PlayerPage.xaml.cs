using DataCollectionManager.DependencyServices;
using DataCollectionManager.MasterDataManager;
using DataCollectionManager.Voice.VoiceUtils;
using DisplayAlertManager.Dialogs;
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

            DataCollectionManager.Music.MusicDataUtils.MusicRecordManager.CurrentReadingRecord.PropertyChanged += CurrentReadingRecord_PropertyChanged;
            
            Player.MediaPlayer.PositionChanged += PlayerPage_PositionChanged;
            Player.MediaQueue.CurrentItemChanged += MediaQueue_CurrentItemChanged;
            InitializeButtons();

        }

        private void CurrentReadingRecord_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            labelMusicRecordQueueCount.Text = DataCollectionManager.Music.MusicDataUtils.MusicRecordManager.Records.Count.ToString();
            labelSensorRecordQueueCount.Text = DataCollectionManager.SmartphoneSensors.SPSensorDataUtils.SPSensorRecordManager.Records.Count.ToString();
            labelVoiceRecordQueueCount.Text = DataCollectionManager.Voice.VoiceDataUtils.VoiceRecordManager.Records.Count.ToString();
        }

        private void InitializeButtons()
        {
            Player.MediaQueue.ShuffleModeChanged += MediaQueue_ShuffleModeChanged;
            Player.MediaQueue.RepeatModeChanged += MediaQueue_RepeatModeChanged;
            Player.PlayerSettings.PropertyChanged += PlayerSettings_PropertyChanged;
            PlayerSettings_PropertyChanged(null, null);
            RepeatModeChangedEventArgs repeatModeChangedEventArgs = new RepeatModeChangedEventArgs() { RepeatMode = Player.MediaQueue.RepeatMode };
            ShuffleModeChangedEventArgs shuffleModeChangedEventArgs = new ShuffleModeChangedEventArgs() { ShuffleMode = Player.MediaQueue.ShuffleMode };
            MediaQueue_RepeatModeChanged(null, repeatModeChangedEventArgs);
            MediaQueue_ShuffleModeChanged(null, shuffleModeChangedEventArgs);
        }

        private void PlayerSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //playback mode
            if (Player.PlayerSettings.IsPlaying)
            {
                PlayerIcons.PlayIcon = "https://i.postimg.cc/wvnXd4My/Icon-Pause.png";
            }
            else if (!(Player.PlayerSettings.IsPlaying))
            {
                PlayerIcons.PlayIcon = "https://i.postimg.cc/44hv1G2z/IconPlay.png";
            }
        }

        private void MediaQueue_RepeatModeChanged(object sender, RepeatModeChangedEventArgs e)
        {
            //repeat settings
            if (e.RepeatMode == RepeatMode.All)
            {
                Player.PlayerSettings.Repeat = RepeatMode.All.ToString();
                PlayerIcons.RepeatIcon = "https://i.postimg.cc/SN6WwrhR/Icon-Repeat-All.png";
            }
            else if (e.RepeatMode == RepeatMode.One)
            {
                Player.PlayerSettings.Repeat = RepeatMode.One.ToString();
                PlayerIcons.RepeatIcon = "https://i.postimg.cc/bwL1vwdj/Icon-Repeat-One.png";
            }
            else if (e.RepeatMode == RepeatMode.Off)
            {
                Player.PlayerSettings.Repeat = RepeatMode.Off.ToString();
                PlayerIcons.RepeatIcon = "https://i.postimg.cc/zGqn5gzj/Icon-Repeat-Off.png";
            }
        }

        private void MediaQueue_ShuffleModeChanged(object sender, ShuffleModeChangedEventArgs e)
        {
            //shuffle settings
            if (e.ShuffleMode == ShuffleMode.All)
            {
                Player.PlayerSettings.Shuffle = ShuffleMode.All.ToString();
                PlayerIcons.ShuffleIcon = "https://i.postimg.cc/FKBcjCjW/Icon-Shuffle-On.png";
            }
            else if (e.ShuffleMode == ShuffleMode.Off)
            {
                Player.PlayerSettings.Shuffle = ShuffleMode.Off.ToString();
                PlayerIcons.ShuffleIcon = "https://i.postimg.cc/W43gqcJq/Icon-Shuffle-Off.png";
            }
        }

        private void MediaQueue_CurrentItemChanged(object sender, MusicPlayer.MusicUtil.CurrentItemChangedEventArgs e)
        {
            labelSongArtist.Text = Player.MediaQueue.CurrentItem.Artist;
            labelSongTitle.Text = Player.MediaQueue.CurrentItem.Title;
            songImage.Source = ImageSource.FromUri(new Uri(Player.MediaQueue.CurrentItem.ImageUri));
            labelDuration.Text = Player.MediaPlayer.Duration.ToString(@"mm\:ss");
        }

        private void PlayerPage_PositionChanged(object sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            Device.InvokeOnMainThreadAsync((() =>
            {
                labelPosition.Text = e.Position.ToString(@"mm\:ss");
                sliderPosition.Value = (e.Position.TotalSeconds / Player.MediaPlayer.Duration.TotalSeconds);
            }));
        }


        private void buttonPlay_Clicked(object sender, EventArgs e)
        {
            if (Player.MediaPlayer.State != MediaPlayerState.Playing)
            {
                Player.Play();
                App.SetRecordTransmit("start");
            }
            else
                Player.Pause();
            App.SetRecordTransmit("stop");

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
    }

}