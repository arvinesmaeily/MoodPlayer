using APIManager;
using APIManager.Account;
using APIManager.Account.Models.Requests;
using APIManager.Account.Models.Responses;
using MoodPlayer.Extensions;
using MoodPlayer.ViewNavigation;
using SettingsManager;
using System;
using System.Collections.Generic;
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
    public partial class LoginEmailView : ContentPage
    {
        //Captcha captcha = new Captcha();
        public LoginEmailView()
        {
            InitializeComponent();

            //LabelCurrentIP.Text = APIManager.Resources.BaseURL;
            //labelCaptcha.BindingContext = captcha;
        }

        private void buttonLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (entryEmail.Text.Length < 1 || entryPassword.Text.Length < 1) //|| entryCaptcha.Text.Length < 1)
                {
                    DisplayAlert("خطا در فیلد", "لطفا همه فیلد ها را پر کنید", "بستن");
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
                    LoginEmailRequest request = new LoginEmailRequest() { Email = entryEmail.Text, Password = entryPassword.Text };
                    Response<LoginEmailResponse> result = AccountManager.EmailLogin(request);
                    if (result.StatusCode != HttpStatusCode.OK)
                    {
                        DisplayAlert("خطای ورود به حساب", result.Content.Error, "بستن");
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
                DisplayAlert("خطای پیشبینی نشده", ex.Message, "بستن");
            }
        }

        private void ButtonSubmitIP_Clicked(object sender, EventArgs e)
        {
            //APIManager.Resources.BaseURL = "http://" + EntryNewIP.Text + "/api";
            //LabelCurrentIP.Text = APIManager.Resources.BaseURL;
        }
    }
}