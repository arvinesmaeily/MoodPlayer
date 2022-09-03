using MusicPlayer.MusicUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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