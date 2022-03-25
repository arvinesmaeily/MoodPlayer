
using DataCollectionManager.DependencyServices;
using MoodPlayer.ViewNavigation;
using MoodPlayer.Views;
using System;
using System.Threading.Tasks;
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
        public static void PresentAlert(string title, string message, string cancel)
        {
            Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static Task<string> PresentPromptAsync(string title, string message, string accept, string cancel)
        {
            return Current.MainPage.DisplayPromptAsync(title, message, accept, cancel);
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
