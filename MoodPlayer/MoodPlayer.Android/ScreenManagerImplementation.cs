using Android.Views;
using DataCollectionManager.DependencyServices;
using MoodPlayer.Droid;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScreenManagerImplementation))]
namespace MoodPlayer.Droid
{
    class ScreenManagerImplementation : IScreenManager
    {
        [MTAThread]
        public void KeepOn()
        {
            MainActivity mainActivity = MainActivity.Instance;
            mainActivity.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            mainActivity.Window.SetSustainedPerformanceMode(true);
            mainActivity.Window.AddFlags(WindowManagerFlags.ShowWhenLocked);
        }

    }
}