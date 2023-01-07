using DataCollectionManager.Music.MusicDataUtils;
using DataCollectionManager.SmartphoneSensors.SPSensorDataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views
{

    public partial class StatePage : ContentPage
    {
        public StatePage()
        {
            InitializeComponent();

            AccelerationLabel.BindingContext = SPSensorRecordManager.CurrentReadingRecord;
            LocationLabel.BindingContext = SPSensorRecordManager.CurrentReadingRecord;


            TitleLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            ArtistLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            AlbumLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            IdLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            DurationLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            PositionLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            RepeatLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            ShuffleLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            VolumeLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
            MuteLabel.BindingContext = MusicRecordManager.CurrentReadingRecord;
        }
    }
}