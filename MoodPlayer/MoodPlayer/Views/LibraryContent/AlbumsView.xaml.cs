using MediaManager.Library;
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
    public partial class AlbumsView : ContentPage
    {
        public AlbumsView()
        {
            InitializeComponent();
            listViewItems.ItemsSource = Library.Data;
        }
    }
}