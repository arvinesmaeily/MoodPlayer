
using MusicPlayer;
using MusicPlayer.MusicUtil;

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
            if (e.SelectedItem != null)
            {
                Library.SetPlaylist(Library.Data);
                Library.BeginningItem(e.SelectedItemIndex);
                if (Player.MediaPlayer.IsPlaying)
                    App.SetRecordTransmit("start");
            }

            listViewItems.SelectedItem = null;
        }

    }
}