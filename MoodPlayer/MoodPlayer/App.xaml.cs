
using APIManager.Account;
using APIManager.Account.Models.Responses;
using DependencyManager;
using DataCollectionManager.MasterDataManager;
using MoodPlayer.ViewNavigation;
using MoodPlayer.Views;
using MusicPlayer;
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

            
            //Player.Current.SetQueue(Library.Data);
        }

        private static void CheckCredentials()
        {
            APIManager.Resources.user_token = AppSettings.AccountSettings.ClientToken;
            if (AppSettings.AccountSettings.ClientAuthorized == true)
            {
                TokenValidationResponse tokenValidationResponse = AccountManager.TokenValidation();
                if (tokenValidationResponse.Code == "200")
                {
                    NavigationManager.GotoMain();
                    DependencyService.Get<IScreenManager>().KeepOn();

                    Player.Initialize();

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
        public static void SetRecordTransmit(string action)
        {
            if (action == "start")
            {
                if (!DataRecordingManager.Status.IsRecording)
                    DataRecordingManager.Set(DataRecordingManager.Mode.RecordingOn);
                if (!DataRecordingManager.Status.IsTransmitting)
                    DataRecordingManager.Set(DataRecordingManager.Mode.TransmittingOn);

            }
            if (action == "stop")
            {
                if (DataRecordingManager.Status.IsRecording)
                    DataRecordingManager.Set(DataRecordingManager.Mode.RecordingOff);
                //if (DataRecordingManager.Status.IsTransmitting)
                    //DataRecordingManager.Set(DataRecordingManager.Mode.TransmittingOff);

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
