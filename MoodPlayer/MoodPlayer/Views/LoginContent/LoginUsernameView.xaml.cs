using APIManager;
using APIManager.Account;
using APIManager.Account.Models.Requests;
using APIManager.Account.Models.Responses;
using MoodPlayer.Extensions;
using MoodPlayer.ViewNavigation;
using SettingsManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static SQLite.SQLite3;

namespace MoodPlayer.Views.LoginContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUsernameView : ContentPage
    {
        //Captcha captcha = new Captcha();
        public LoginUsernameView()
        {
            InitializeComponent();

            //labelCaptcha.BindingContext = captcha;
        }

        private void buttonLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (entryUsername.Text.Length < 1 || entryPassword.Text.Length < 1) // || entryCaptcha.Text.Length < 1)
                {
                    DisplayAlert("Entry Field Error", "Please fill all the entry fields correctly.", "Okay");
                }
                else
                {
                    //if (entryCaptcha.Text.ToLower() != captcha.Term.ToLower())
                    //{
                    //    DisplayAlert("Captcha Error", "Please enter the captcha term correctly. Captcha is not case sensitive.", "Okay");
                    //    captcha.Generate();
                    //}
                    //else
                    //{
                    LoginUsernameRequest request = new LoginUsernameRequest() { Username = entryUsername.Text, Password = entryPassword.Text };
                    Response<LoginUsernameResponse> result = AccountManager.UsernameLogin(request);
                    Debug.WriteLine(result.Content.Token);
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        DisplayAlert("Login Error", result.Content.Error, "Okay");
                        //captcha.Generate();
                    }
                    else
                    {
                        var Token = "Token " + result.Content.Token;
                        AppSettings.AccountSettings.ClientAuthorized = true;
                        AppSettings.AccountSettings.ClientToken = Token;
                        AppSettings.AccountInfo.ID = result.Content.UserId;

                        APIManager.Resources.Authorization = Token;
                        //captcha.Generate();
                        NavigationManager.GotoMain();
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Exception Occured!", ex.Message, "Okay");
            }
        }
    }
}