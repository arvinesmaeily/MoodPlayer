using APIManager.Account;
using APIManager.Account.Models.Responses;
using MoodPlayer.Extensions;
using MoodPlayer.ViewNavigation;
using SettingsManager;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoodPlayer.Views.LoginContent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        Captcha captcha = new Captcha();
        public RegisterView()
        {

            InitializeComponent();
            labelCaptcha.BindingContext = captcha;

        }

        private void buttonLogin_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                if (entryUsername.Text.Length < 1 || entryEmail.Text.Length < 1 || entryPassword.Text.Length < 1 || entrySecondPassword.Text.Length < 1 || entryCaptcha.Text.Length < 1)
                {
                    DisplayAlert("Entry Field Error", "Please fill all the entry fields correctly.", "Okay");
                    captcha.Generate();
                }
                else
                {
                    if (entryCaptcha.Text.ToLower() != captcha.Term.ToLower())
                    {
                        DisplayAlert("Captcha Error", "Please enter the captcha term correctly. Captcha is not case sensitive.", "Okay");
                        captcha.Generate();
                    }
                    else if (entryPassword.Text != entrySecondPassword.Text)
                    {
                        entryPassword.Text = "";
                        entrySecondPassword.Text = "";
                        DisplayAlert("Password Error", "Passwords do not match. Please enter passwords again.", "Okay");
                        captcha.Generate();
                    }
                    else
                    {
                        SignupResponse result = AccountManager.SignUp(entryUsername.Text, entryEmail.Text, entryPassword.Text).Result;
                        if (result.code != "200")
                        {
                            DisplayAlert("Signup Error", result.error, "Okay");
                            captcha.Generate();
                        }
                        else
                        {
                            DisplayAlert("Successful Registration!", "Your account has been created successfully!", "Okay");
                            captcha.Generate();
                            LoginUsernameResponse result2 = AccountManager.UsernameLogin(entryUsername.Text, entryPassword.Text).Result;
                            if (result2.code != "200")
                            {
                                DisplayAlert("Login Error", result2.error, "Okay");
                                DisplayAlert("Manual Login", "Please login manually.", "Okay");
                                captcha.Generate();
                            }
                            else
                            {
                                AppSettings.AccountSettings.ClientAuthorized = true;
                                AppSettings.AccountSettings.ClientToken = result2.data.user.accessTokens[result2.data.user.accessTokens.Length - 1];

                                AppSettings.AccountInfo.ID = result2.data.user.id;
                                AppSettings.AccountInfo.Username = result2.data.user.username;
                                AppSettings.AccountInfo.Email = result2.data.user.email;
                                AppSettings.AccountInfo.LastLogin = result2.data.user.lastLogin;
                                NavigationManager.GotoMain();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Exception Occured!", ex.Message, "Okay");
            }
        }
    }
}