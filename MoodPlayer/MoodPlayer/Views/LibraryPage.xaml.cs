﻿using MusicPlayer.MusicUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibraryPage : Shell
    {

        public LibraryPage()
        {
            InitializeComponent();

        }

        override protected void OnAppearing()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlertManager.AlertPresenter.PresentToast("Loading List...", Int32.MaxValue);
                Library.Load();
                DisplayAlertManager.AlertPresenter.PresentToast("List Loaded Successfully!", Int32.MaxValue);
            });
        }
    }
}