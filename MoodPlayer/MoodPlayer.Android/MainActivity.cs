using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using AndroidX.Core.Content;
using AndroidX.Core.App;


namespace MoodPlayer.Droid
{
    [Activity(LaunchMode = LaunchMode.SingleTop,Label = "MoodPlayer", Icon = "@mipmap/icon", Theme = "@style/MyTheme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; internal set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            base.OnCreate(savedInstanceState);


            PrepareAlertManager();
            PrepareMusicPlayer();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Window.SetStatusBarColor(Android.Graphics.Color.Black);
            RequirePermissions();
        }

        private void PrepareMusicPlayer()
        {
            ZPF.Media.MediaPlayer.Current.Init(this);
        }

        private void PrepareAlertManager()
        {
            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            AiForms.Dialogs.Dialogs.Init(this); //need to write here
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void RequirePermissions()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.RecordAudio) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] {
                    Manifest.Permission.AccessNetworkState,
                    Manifest.Permission.Internet,
                    Manifest.Permission.AccessFineLocation,
                    Manifest.Permission.AccessCoarseLocation,
                    Manifest.Permission.AccessMockLocation,
                    Manifest.Permission.AccessMediaLocation,
                    Manifest.Permission.AccessLocationExtraCommands,
                    Manifest.Permission.AccessBackgroundLocation,
                    Manifest.Permission.LocationHardware,
                    Manifest.Permission.ChangeWifiState,
                    Manifest.Permission.AccessWifiState,
                    Manifest.Permission.ChangeWifiMulticastState,
                    Manifest.Permission.ForegroundService,
                    Manifest.Permission.ManageMedia,
                    Manifest.Permission.ModifyAudioSettings,
                    Manifest.Permission.RecordAudio,
                    Manifest.Permission_group.Display,
                    Manifest.Permission_group.HardwareControls,
                    Manifest.Permission_group.Microphone,
                    Manifest.Permission_group.Network,
                    Manifest.Permission_group.Sensors,
                    Manifest.Permission_group.Screenlock
                }, 1);
            }
        }
    }
}