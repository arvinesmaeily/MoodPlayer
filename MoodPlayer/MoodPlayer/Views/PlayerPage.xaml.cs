using MoodPlayer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views
{
    public partial class PlayerPage : ContentPage
    {


        public PlayerPage()
        {
            InitializeComponent();
            InitializeInterface();
        }

        private void InitializeInterface()
        {
            NervousnessColorChange();

            Settings.AppSettings.DataCollectionSettings.PropertyChanged += DataCollectionSettings_PropertyChanged;
            DataCollectionSettings_PropertyChanged(null, new PropertyChangedEventArgs(nameof(Settings.AppSettings.DataCollectionSettings.CollectSpotify)));
            DataCollectionSettings_PropertyChanged(null, new PropertyChangedEventArgs(nameof(Settings.AppSettings.DataCollectionSettings.CollectSPSensor)));
            DataCollectionSettings_PropertyChanged(null, new PropertyChangedEventArgs(nameof(Settings.AppSettings.DataCollectionSettings.CollectVoice)));
        }

        private void NervousnessColorChange()
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void DataCollectionSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var enableColor = Color.LimeGreen;
            var disableColor = Color.OrangeRed;
            var grayedoutColor = Color.LightGray;

            if(e.PropertyName == nameof(Settings.AppSettings.DataCollectionSettings.CollectSpotify))
            {
                if(Settings.AppSettings.DataCollectionSettings.CollectSpotify == true)
                {
                    SpotifyToggle.BackgroundColor = enableColor;
                }
                else if(Settings.AppSettings.DataCollectionSettings.CollectSpotify == false)
                {
                    SpotifyToggle.BackgroundColor = disableColor;
                }
                else
                {
                    SpotifyToggle.BackgroundColor = grayedoutColor;
                }
            }

            if (e.PropertyName == nameof(Settings.AppSettings.DataCollectionSettings.CollectVoice))
            {
                if (Settings.AppSettings.DataCollectionSettings.CollectVoice == true)
                {
                    VoiceToggle.BackgroundColor = enableColor;
                }
                else if (Settings.AppSettings.DataCollectionSettings.CollectVoice == false)
                {
                    VoiceToggle.BackgroundColor = disableColor;
                }
                else
                {
                    VoiceToggle.BackgroundColor = grayedoutColor;
                }
            }

            if (e.PropertyName == nameof(Settings.AppSettings.DataCollectionSettings.CollectSPSensor))
            {
                if (Settings.AppSettings.DataCollectionSettings.CollectSPSensor == true)
                {
                    SPSensorToggle.BackgroundColor = enableColor;
                }
                else if (Settings.AppSettings.DataCollectionSettings.CollectSPSensor == false)
                {
                    SPSensorToggle.BackgroundColor = disableColor;
                }
                else
                {
                    SPSensorToggle.BackgroundColor = grayedoutColor;
                }
            }
        }

        private void SpotifyToggle_Clicked(object sender, EventArgs e)
        {
            if (Settings.AppSettings.DataCollectionSettings.CollectSpotify == true)
                Settings.AppSettings.DataCollectionSettings.CollectSpotify = false;
            else
                Settings.AppSettings.DataCollectionSettings.CollectSpotify = true;
        }

        private void SPSensorToggle_Clicked(object sender, EventArgs e)
        {
            if (Settings.AppSettings.DataCollectionSettings.CollectSPSensor == true)
                Settings.AppSettings.DataCollectionSettings.CollectSPSensor = false;
            else
                Settings.AppSettings.DataCollectionSettings.CollectSPSensor = true;
        }

        private void VoiceToggle_Clicked(object sender, EventArgs e)
        {
            if (Settings.AppSettings.DataCollectionSettings.CollectVoice == true)
                Settings.AppSettings.DataCollectionSettings.CollectVoice = false;
            else
                Settings.AppSettings.DataCollectionSettings.CollectVoice = true;
        }
    }
}