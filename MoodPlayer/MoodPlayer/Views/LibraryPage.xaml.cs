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
            
        }

        override protected void OnAppearing()
        {

            /*try
            {
                Library.Load();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Data);
                Debug.WriteLine(e.HelpLink);
                Debug.WriteLine(e.HResult);
                Debug.WriteLine(e.InnerException);
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.Source);
                Debug.WriteLine(e.StackTrace);
                Debug.WriteLine(e.TargetSite);
            }*/
        }
    }
}