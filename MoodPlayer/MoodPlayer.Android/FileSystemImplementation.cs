using Android.Content;
using DependencyManager;
using MoodPlayer.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystemImplementation))]

namespace MoodPlayer.Droid
{
    public class FileSystemImplementation : IFileSystem
    {
        public string GetExternalStorage()
        {
            Context context = Android.App.Application.Context;
            var filePath = context.GetExternalFilesDir("");
            return filePath.Path;

        }
    }
}