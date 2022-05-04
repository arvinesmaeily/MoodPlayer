
using APIManager.Account;
using APIManager.Account.Models.Responses;
using DataCollectionManager.DependencyServices;
using MoodPlayer.ViewNavigation;
using MoodPlayer.Views;
using MusicPlayer.MusicUtil;
using SettingsManager;
using System;
using System.Diagnostics;
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
            CheckCredentials();
            var color = (Color)Current.Resources["Secondary"];

            MusicPlayer.Models.MusicData.ThemeColor = (color.ToHex().Remove(0,3));

            Library.Load();
            Player.Current.SetQueue(Library.Data);
        }

        private static void CheckCredentials()
        {
            APIManager.Resources.user_token = AppSettings.AccountSettings.ClientToken;
            if (AppSettings.AccountSettings.ClientAuthorized == true)
            {
                TokenValidationResponse tokenValidationResponse = AccountManager.TokenValidation().Result;
                if (tokenValidationResponse.Code == "200")
                {
                    NavigationManager.GotoMain();
                    DependencyService.Get<IScreenManager>().KeepOn();


                }
                else
                {
                    NavigationManager.GotoLogin();
                    DisplayAlertManager.AlertPresenter.PresentToast("Authorization Failed" + "\nError Code: " + tokenValidationResponse.Code + "\n Error Message: " + tokenValidationResponse.Error, Int32.MaxValue);

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
