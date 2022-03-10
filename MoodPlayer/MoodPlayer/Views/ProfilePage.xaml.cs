using MoodPlayer.ViewNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            
            id.BindingContext = Settings.AppSettings.AccountInfo;
            username.BindingContext = Settings.AppSettings.AccountInfo;
            email.BindingContext = Settings.AppSettings.AccountInfo;
            lastLogin.BindingContext = Settings.AppSettings.AccountInfo;

            stateSpotify.BindingContext = Settings.AppSettings.AccountSettings;
            spotifyToken.BindingContext = Settings.AppSettings.AccountSettings;
        }

        private void buttonConnectSpotify_Clicked(object sender, EventArgs e)
        {

        }

        private void buttonLogout_Clicked(object sender, EventArgs e)
        {
            Settings.AppSettings.AccountInfo.Clear();
            Settings.AppSettings.AccountSettings.Clear();
            NavigationManager.GotoLogin();
        }
    }
}