using APIManager.Account;
using APIManager.Account.Models.Responses;
using MoodPlayer.Extensions;
using MoodPlayer.ViewNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views.LoginContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginEmailView : ContentPage
    {
        //Captcha captcha = new Captcha();
        public LoginEmailView()
        {
            InitializeComponent();

            //labelCaptcha.BindingContext = captcha;
        }

        private void buttonLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (entryEmail.Text.Length < 1 || entryPassword.Text.Length < 1) //|| entryCaptcha.Text.Length < 1)
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
                    LoginEmailResponse result = AccountManager.EmailLogin(entryEmail.Text, entryPassword.Text).Result;
                    if (result.code != "200")
                    {
                        DisplayAlert("Login Error", result.error, "Okay");
                        //captcha.Generate();
                    }
                    else
                    {
                        Settings.AppSettings.AccountSettings.ClientAuthorized = true;
                        Settings.AppSettings.AccountSettings.ClientToken = result.data.user.accessTokens[result.data.user.accessTokens.Length - 1];

                        Settings.AppSettings.AccountInfo.ID = result.data.user.id;
                        Settings.AppSettings.AccountInfo.Username = result.data.user.username;
                        Settings.AppSettings.AccountInfo.Email = result.data.user.email;
                        Settings.AppSettings.AccountInfo.LastLogin = result.data.user.lastLogin;
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