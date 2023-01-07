using DataCollectionManager.MasterDataManager;

using MusicPlayer;
using MusicPlayer.MusicUtil;
using SettingsManager;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using static MusicPlayer.Player;

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

            labelSongTitle.BindingContext = Player.MediaQueue.CurrentItem;
            labelSongArtist.BindingContext = Player.MediaQueue.CurrentItem;

            



            Player.MediaPlayer.Paused += MediaPlayer_Paused;
            Player.MediaPlayer.Playing += MediaPlayer_Playing;

            Player.MediaPlayer.EncounteredError += MediaPlayer_EncounteredError;
            Player.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;
            LabelStatus.BindingContext = DataRecordingManager.Status;
            labelRemaining.BindingContext = DataRecordingManager.Status;

            Player.MediaQueue.CurrentItemChanged += MediaQueue_CurrentItemChanged;

        }

        private void MediaPlayer_PositionChanged(object sender, LibVLCSharp.Shared.MediaPlayerPositionChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread((() =>
            {
                long Duration = MediaPlayer.Length;
                long Position = Convert.ToInt64(e.Position * Duration);

                labelDuration.Text = TimeSpan.FromMilliseconds(Duration).ToString(@"mm\:ss");
                labelPosition.Text = TimeSpan.FromMilliseconds(Position).ToString(@"mm\:ss");
                progressBarPosition.Progress = e.Position;
            }));
        }

        private void MediaPlayer_EncounteredError(object sender, EventArgs e)
        {
            DisplayAlert("پخش با مشکل مواجه شد.", "برنامه را مجددا اجرا کنید", "بستن").ContinueWith((x) => { Application.Current.Quit(); });
        }

        private void MediaPlayer_Playing(object sender, EventArgs e)
        {
            PlayerIcons.PlayIcon = new FileImageSource()
            {
                File = "iconpause.png"
            };
        }

        private void MediaPlayer_Paused(object sender, EventArgs e)
        {
            PlayerIcons.PlayIcon = new FileImageSource()
            {
                File = "iconplay.png"
            };
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
                //if (Player.MediaPlayer.State != MediaPlayerState.Playing)
                {
                    Player.Play();
                    App.SetRecordTransmit("start");
                }
                //else
                {
                    Player.Pause();
                    App.SetRecordTransmit("stop");
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
        }
    }

}