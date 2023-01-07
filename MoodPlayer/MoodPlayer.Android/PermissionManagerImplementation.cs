using Android.Views;
using DependencyManager;
using MoodPlayer.Droid;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(PermissionManagerImplementation))]
namespace MoodPlayer.Droid
{
    internal class PermissionManagerImplementation : IPermissionManager
    {
        public async void CheckPermissions()
        {
            var res = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
            if(res != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.LocationAlways>();
            }

            res = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (res != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.LocationAlways>();
            }

            res = await Permissions.CheckStatusAsync<Permissions.Sensors>();
            if (res != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.LocationAlways>();
            }
        }
    }
}