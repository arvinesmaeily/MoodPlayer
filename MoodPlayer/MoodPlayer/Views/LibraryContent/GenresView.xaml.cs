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
    public partial class GenresView : ContentPage
    {
        public GenresView()
        {
            InitializeComponent();
            listViewItems.ItemsSource = Library.Data;
        }
    }
}