using DataCollectionManager.DependencyServices;
using DataCollectionManager.MasterDataManager;
using DataCollectionManager.Voice.VoiceUtils;
using DisplayAlertManager.Dialogs;
using MoodPlayer.Extensions;
using MoodPlayer.Views;
using MusicPlayer;
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
using ZPF.Media;

namespace MoodPlayer.Views
{
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            InitializeComponent();

            Player.GetMediaPlayer().MediaItemChanged += PlayerPage_MediaItemChanged;
            Player.GetMediaPlayer().PositionChanged += PlayerPage_PositionChanged;
            Player.PlayerSettings.PropertyChanged += PlayerSettings_PropertyChanged;
        }

        private void PlayerPage_MediaItemChanged(object sender, MediaItemEventArgs e)
        {

            labelSongArtist.Text = Player.GetMediaPlayer().Playlist.Current.Artist;
            labelSongTitle.Text = Player.GetMediaPlayer().Playlist.Current.Title;
            songImage.Source = Player.GetMediaPlayer().Playlist.Current.ArtUri;
        }

        private void PlayerSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetButtonsIcons();
        }

        private void PlayerPage_PositionChanged(object sender, ZPF.Media.PositionChangedEventArgs e)
        {
            Task.Run(() =>
            {
                labelDuration.Text = e.Duration.ToString(@"mm\:ss");
                labelPosition.Text = e.Position.ToString(@"mm\:ss");


            });

            Task.Run(() =>
            {
                sliderPosition.Value = (e.Position.TotalSeconds / e.Duration.TotalSeconds);
            });   

        }

        private void SetButtonsIcons()
        {
            //shuffle settings
            if (Player.GetMediaPlayer().Playlist.ShuffleMode == ShuffleMode.On)
            {
                Debug.WriteLine(Player.GetMediaPlayer().Playlist.ShuffleMode);
                buttonShuffle.Source = ImageSource.FromUri(new Uri("https://i.postimg.cc/FKBcjCjW/Icon-Shuffle-On.png"));
            }
            else if (Player.GetMediaPlayer().Playlist.ShuffleMode == ShuffleMode.Off)
            {
                Debug.WriteLine(Player.GetMediaPlayer().Playlist.ShuffleMode);
                buttonShuffle.Source = ImageSource.FromUri(new Uri("https://i.postimg.cc/W43gqcJq/Icon-Shuffle-Off.png"));
            }

            //repeat settings
            if (Player.GetMediaPlayer().Playlist.RepeatMode == RepeatMode.All)
            {
                Debug.WriteLine(Player.GetMediaPlayer().Playlist.RepeatMode);
                buttonRepeat.Source = ImageSource.FromUri(new Uri("https://i.postimg.cc/SN6WwrhR/Icon-Repeat-All.png"));
            }
            else if (Player.GetMediaPlayer().Playlist.RepeatMode == RepeatMode.One)
            {
                Debug.WriteLine(Player.GetMediaPlayer().Playlist.RepeatMode);
                buttonRepeat.Source = ImageSource.FromUri(new Uri("https://i.postimg.cc/bwL1vwdj/Icon-Repeat-One.png"));
            }
            else if (Player.GetMediaPlayer().Playlist.RepeatMode == RepeatMode.Off)
            {
                Debug.WriteLine(Player.GetMediaPlayer().Playlist.RepeatMode);
                buttonRepeat.Source = ImageSource.FromUri(new Uri("https://i.postimg.cc/zGqn5gzj/Icon-Repeat-Off.png"));
            }

            //playback mode
            if (Player.GetMediaPlayer().State == MediaPlayerState.Playing)
            {
                buttonPlay.Source = ImageSource.FromUri(new Uri("https://i.postimg.cc/wvnXd4My/Icon-Pause.png"));
            }
            else if(!(Player.GetMediaPlayer().State == MediaPlayerState.Playing))
            {
                buttonPlay.Source = ImageSource.FromUri(new Uri("https://i.postimg.cc/44hv1G2z/IconPlay.png"));
            }
        }





        /*        private void NervousnessColorChange()
                {
                    Task.Run(() =>
                    {
                        int rMax = (int)(Color.Red.R * 255);
                        int rMid = (int)(Color.White.R * 255);
                        int rMin = (int)(Color.Blue.R * 255);
                        int gMax = (int)(Color.Red.G * 255);
                        int gMid = (int)(Color.White.G * 255);
                        int gMin = (int)(Color.Blue.G * 255);
                        int bMax = (int)(Color.Red.B * 255);
                        int bMid = (int)(Color.White.B * 255);
                        int bMin = (int)(Color.Blue.B * 255);

                        int size = 50;

                        var colorList = new List<Color>();
                        for (int i = 0; i < size; i++)
                        {
                            var rAverage = rMin + ((rMid - rMin) * i / size);
                            var gAverage = gMin + ((gMid - gMin) * i / size);
                            var bAverage = bMin + ((bMid - bMin) * i / size);
                            colorList.Add(Color.FromRgb(rAverage, gAverage, bAverage));
                        }
                        for (int i = 0; i < size; i++)
                        {
                            var rAverage = rMid + ((rMax - rMid) * i / size);
                            var gAverage = gMid + ((gMax - gMid) * i / size);
                            var bAverage = bMid + ((bMax - bMid) * i / size);
                            colorList.Add(Color.FromRgb(rAverage, gAverage, bAverage));
                        }
                        int j = 0;
                        while (true)
                        {
                            if (j == 0)
                            {
                                for (j = 0; j < colorList.Count; j++)
                                {
                                    Resources["nervousness"] = colorList[j];
                                    Task.Delay(100).Wait();
                                }
                                j = colorList.Count - 1;
                            }
                            if (j == (colorList.Count - 1))
                            {
                                for (j = colorList.Count - 1; j >= 0; j--)
                                {
                                    Resources["nervousness"] = colorList[j];
                                    Task.Delay(100).Wait();
                                }
                                j = 0;
                            }
                        }

                    });
                }*/

        private void DataCollectionSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var enableColor = Color.LimeGreen;
            var disableColor = Color.OrangeRed;
            var grayedoutColor = Color.LightGray;

            if (e.PropertyName == nameof(AppSettings.DataCollectionSettings.CollectVoice))
            {
                if (AppSettings.DataCollectionSettings.CollectVoice == true)
                {
                    //VoiceToggle.BackgroundColor = enableColor;
                }
                else if (AppSettings.DataCollectionSettings.CollectVoice == false)
                {
                    //VoiceToggle.BackgroundColor = disableColor;
                }
                else
                {
                    //VoiceToggle.BackgroundColor = grayedoutColor;
                }
            }

        }

        private void VoiceToggle_Clicked(object sender, EventArgs e)
        {
            if (AppSettings.DataCollectionSettings.CollectVoice == true)
                AppSettings.DataCollectionSettings.CollectVoice = false;
            else
                AppSettings.DataCollectionSettings.CollectVoice = true;
        }

        private void LearningToggle_Clicked(object sender, EventArgs e) 
        {
            
        }

        private void buttonPlay_Clicked(object sender, EventArgs e)
        {
            
            Player.PlayPause();
            if(Player.GetMediaPlayer().State == MediaPlayerState.Playing)
            App.SetRecordTransmit("start");

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
            if (Player.GetMediaPlayer().Playlist.Count > 0)
            {
                if (Player.GetMediaPlayer().Playlist.ShuffleMode == ShuffleMode.Off)
                {
                    Player.GetMediaPlayer().Playlist.ShuffleMode = ShuffleMode.On;
                    Player.PlayerSettings.Shuffle = Player.GetMediaPlayer().Playlist.ShuffleMode.ToString();
                }
                else
                {
                    Player.GetMediaPlayer().Playlist.ShuffleMode = ShuffleMode.Off;
                    Player.PlayerSettings.Shuffle = Player.GetMediaPlayer().Playlist.ShuffleMode.ToString();
                }
            }
        }
        private void buttonFavorite_Clicked(object sender, EventArgs e)
        {

        }

        private void buttonRepeat_Clicked(object sender, EventArgs e)
        {
            if (Player.GetMediaPlayer().Playlist.Count > 0)
            {

                if (Player.GetMediaPlayer().Playlist.RepeatMode == RepeatMode.Off)
                {
                    Player.GetMediaPlayer().Playlist.RepeatMode = RepeatMode.One;
                    Player.PlayerSettings.Repeat = Player.GetMediaPlayer().Playlist.RepeatMode.ToString();
                }
                else if (Player.GetMediaPlayer().Playlist.RepeatMode == RepeatMode.One)
                {
                    Player.GetMediaPlayer().Playlist.RepeatMode = RepeatMode.All;
                    Player.PlayerSettings.Repeat = Player.GetMediaPlayer().Playlist.RepeatMode.ToString();
                }
                else if (Player.GetMediaPlayer().Playlist.RepeatMode == RepeatMode.All)
                {
                    Player.GetMediaPlayer().Playlist.RepeatMode = RepeatMode.Off;
                    Player.PlayerSettings.Repeat = Player.GetMediaPlayer().Playlist.RepeatMode.ToString();
                }
            }
        }
    }

}