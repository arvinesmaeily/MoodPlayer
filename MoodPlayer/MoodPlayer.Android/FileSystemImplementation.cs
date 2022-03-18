using Android.Content;
using DataCollectionManager.DependencyServices;
using MoodPlayer.Droid;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystemImplementation))]
namespace MoodPlayer.Droid
{
    public class FileSystemImplementation : IFileSystem
    {
        [MTAThread]
        public string GetExternalStorage()
        {
            Context context = Android.App.Application.Context;
            var filePath = context.GetExternalFilesDir("");
            return filePath.Path;
        }
    }

}