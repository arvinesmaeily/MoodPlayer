
using MoodPlayer.ViewNavigation;
using MoodPlayer.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            if (Settings.AppSettings.AccountSettings.ClientAuthorized == true)
            {
                NavigationManager.GotoMain();
            }
            else
            {
                NavigationManager.GotoLogin();
            }
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
