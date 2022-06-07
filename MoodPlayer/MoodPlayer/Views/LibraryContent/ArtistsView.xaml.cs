using MusicPlayer.MusicUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MoodPlayer.Views.LibraryContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtistsView : ContentPage
    {
        public ArtistsView()
        {
            InitializeComponent();
            listViewItems.ItemsSource = Library.Artists;
        }

        private void listViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is ArtistItem item)
            {
                var List = Library.ArtistsDict[item.Artist];

                this.Navigation.PushAsync(new SelectedItemView(item.Artist, List));
            }
            listViewItems.SelectedItem = null;
        }
    }
}