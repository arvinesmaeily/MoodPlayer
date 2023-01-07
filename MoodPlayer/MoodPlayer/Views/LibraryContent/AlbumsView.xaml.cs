
using MusicPlayer.MusicUtil;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views.LibraryContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumsView : ContentPage
    {
        public AlbumsView()
        {
            InitializeComponent();

            listViewItems.ItemsSource = Library.Albums;

        }

        private void listViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as AlbumItem;

            if (item != null)
            {
                var List = Library.AlbumsDict[item.Album];

                this.Navigation.PushAsync(new SelectedItemView(item.Album, List));
            }
            listViewItems.SelectedItem = null;
        }
    }
}