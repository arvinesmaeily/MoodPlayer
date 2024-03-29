﻿using MoodPlayer.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoodPlayer.ViewNavigation
{
    public static class NavigationManager
    {
        public static AppShell MainPage = new AppShell();
        public static LoginPage LoginPage = new LoginPage(); 

        public static void GotoLogin()
        {
            Application.Current.MainPage = LoginPage;
        }
        public static void GotoMain()
        {
            Application.Current.MainPage = MainPage;
        }
    }
}
