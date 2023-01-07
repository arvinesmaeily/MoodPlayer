
using MusicPlayer;
using MusicPlayer.MusicUtil;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views.LibraryContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedItemView : ContentPage
    {
        string Key = "";
        List<MediaItem> MediaItems = new List<MediaItem>();
        public SelectedItemView()
        {
            InitializeComponent();
        }
        public SelectedItemView(string key, List<MediaItem> mediaItems)
        {
            InitializeComponent();
            Key = key;
            MediaItems = mediaItems;
            listViewItems.ItemsSource = MediaItems;
        }

        private void listViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Library.SetPlaylist(MediaItems);
            Library.BeginningItem(e.SelectedItemIndex);
            if (Player.MediaPlayer.State != LibVLCSharp.Shared.VLCState.Playing)
                App.SetRecordTransmit("start");

            listViewItems.SelectedItem = null;

        }
    }
}