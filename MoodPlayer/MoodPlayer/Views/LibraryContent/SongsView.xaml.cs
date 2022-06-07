using MediaManager.Library;
using MediaManager.Player;
using MusicPlayer;
using MusicPlayer.Models;
using MusicPlayer.MusicUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MoodPlayer.Views.LibraryContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongsView : ContentPage
    {
        public SongsView()
        {
            InitializeComponent();
            listViewItems.ItemsSource = Library.Data;
        }


        private void listViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                Library.SetPlaylist(Library.Data);
                Library.BeginningItem(e.SelectedItemIndex);
                if (Player.MediaPlayer.State != MediaPlayerState.Playing)
                    App.SetRecordTransmit("start");
            }

            listViewItems.SelectedItem = null;
        }

    }
}