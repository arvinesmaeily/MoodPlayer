using MoodPlayer.ViewNavigation;
using SettingsManager;
using System;
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
                        DisplayAlert("خروج موفقیت آمیز", "با موفقیت از حساب خود خارج شدید.", "بستن");
                        AppSettings.AccountInfo.Clear();
                        AppSettings.AccountSettings.Clear();
                        NavigationManager.GotoLogin();
                    }
                    else
                    {
                        DisplayAlert("خطا", "Code: " + response.StatusCode + "\nMessage: " + response.Content.Error, "بستن");
                    }
                }
                else
                {
                    DisplayAlert("خطا", "No Response Reecieved.", "بستن");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("خطا", ex.Message, "بستن");
            }
        }
    }
}