
using DataCollectionManager.DependencyServices;
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
                var tokenValidationResponse = APIManager.Account.AccountManager.TokenValidation().Result;
                if (tokenValidationResponse.Code == "200")
                {
                    NavigationManager.GotoMain();
                    DependencyService.Get<IScreenManager>().KeepOn();
                    APIManager.Resources.user_token = Settings.AppSettings.AccountSettings.ClientToken;
                }
                else
                {
                    MainPage.DisplayAlert("Authorization Failed", tokenValidationResponse.Error, "Okay");
                }
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
