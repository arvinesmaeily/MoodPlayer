using MusicPlayer.MusicUtil;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibraryPage : Shell
    {

        public LibraryPage()
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(() =>
            {
                Library.Load();
            });
        }

        override protected void OnAppearing()
        {


        }
    }
}