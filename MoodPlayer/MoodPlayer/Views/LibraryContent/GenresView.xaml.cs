
using MusicPlayer.MusicUtil;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views.LibraryContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenresView : ContentPage
    {
        public GenresView()
        {
            InitializeComponent();
            listViewItems.ItemsSource = Library.Genres;
        }

        private void listViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is GenreItem item)
            {
                var List = Library.GenresDict[item.Genre];

                this.Navigation.PushAsync(new SelectedItemView(item.Genre, List));

            }
            listViewItems.SelectedItem = null;
        }
    }
}