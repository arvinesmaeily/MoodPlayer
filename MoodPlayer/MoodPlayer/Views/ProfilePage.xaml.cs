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

            //stateSpotify.BindingContext = AppSettings.AccountSettings;
            //spotifyToken.BindingContext = AppSettings.AccountSettings;
        }

        private void buttonLogout_Clicked(object sender, EventArgs e)
        {
            try
            {


                var response = APIManager.Account.AccountManager.Logout();
                if (response != null)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        DisplayAlert("Log Out Successful!", "Successfully logged out from your account.", "Done");
                        AppSettings.AccountInfo.Clear();
                        AppSettings.AccountSettings.Clear();
                        NavigationManager.GotoLogin();
                    }
                    else
                    {
                        DisplayAlert("Error", "Code: " + response.StatusCode + "\nMessage: " + response.Content.Error, "Done");
                    }
                }
                else
                {
                    DisplayAlert("Error", "No Response Reecieved.", "Done");
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Done");
            }
        }
    }
}