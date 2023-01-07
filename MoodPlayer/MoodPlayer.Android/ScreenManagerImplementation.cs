using Android.Views;
using DependencyManager;
using MoodPlayer.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScreenManagerImplementation))]
namespace MoodPlayer.Droid
{
    class ScreenManagerImplementation : IScreenManager
    {

        public void KeepOn()
        {
            MainActivity mainActivity = MainActivity.Instance;
            mainActivity.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            mainActivity.Window.SetSustainedPerformanceMode(true);
            mainActivity.Window.AddFlags(WindowManagerFlags.ShowWhenLocked);
        }

    }
}