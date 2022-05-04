using MoodPlayer.ViewNavigation;
using SettingsManager;
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
            
            id.BindingContext = AppSettings.AccountInfo;
            username.BindingContext = AppSettings.AccountInfo;
            email.BindingContext = AppSettings.AccountInfo;
            lastLogin.BindingContext = AppSettings.AccountInfo;

            //stateSpotify.BindingContext = AppSettings.AccountSettings;
            //spotifyToken.BindingContext = AppSettings.AccountSettings;
        }

        private void buttonLogout_Clicked(object sender, EventArgs e)
        {
            AppSettings.AccountInfo.Clear();
            AppSettings.AccountSettings.Clear();
            NavigationManager.GotoLogin();
        }
    }
}