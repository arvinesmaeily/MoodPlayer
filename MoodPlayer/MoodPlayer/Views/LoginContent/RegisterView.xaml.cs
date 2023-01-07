using APIManager;
using APIManager.Account;
using APIManager.Account.Models.Requests;
using APIManager.Account.Models.Responses;
using MoodPlayer.Extensions;
using MoodPlayer.ViewNavigation;
using SettingsManager;
using System;
using System.Net;
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
                    DisplayAlert("خطا در فیلد", "لطفا همه فیلد ها را پر کنید", "بستن");
                    captcha.Generate();
                }
                else
                {
                    if (entryCaptcha.Text.ToLower() != captcha.Term.ToLower())
                    {
                        DisplayAlert("خطای Captcha", "عبارت امنیتی اشتباه وارد شده است. مجددا تلاش کنید.", "بستن");
                        captcha.Generate();
                    }
                    else if (entryPassword.Text != entrySecondPassword.Text)
                    {
                        entryPassword.Text = "";
                        entrySecondPassword.Text = "";
                        DisplayAlert("خطای رمز عبور", "رمز های عبور با یکدیگر مطابقت ندارند. مجددا رمز ها را وارد کنید.", "بستن");
                        captcha.Generate();
                    }
                    else
                    {
                        var request = new SignupRequest() { Username = entryUsername.Text, Email = entryEmail.Text, Password = entryPassword.Text };
                        Response<SignupResponse> result = AccountManager.SignUp(request);

                        if (result.StatusCode != HttpStatusCode.OK)
                        {
                            DisplayAlert("خطای ساختن حساب", result.Content.Error, "بستن");
                            captcha.Generate();
                        }
                        else
                        {
                            DisplayAlert("ایجاد حساب موفقیت آمیز", "حساب شما با موفقیت ایجاد شد.", "بستن");
                            captcha.Generate();

                            var request2 = new LoginUsernameRequest() { Username = entryUsername.Text, Password = entryPassword.Text };
                            Response<LoginUsernameResponse> result2 = AccountManager.UsernameLogin(request2);

                            if (result2.StatusCode != HttpStatusCode.OK)
                            {
                                DisplayAlert("خطای ورود به حساب", result2.Content.Error, "بستن");
                                DisplayAlert("وارد شدن دستی", "ورود اتوماتیک موفقیت آمیز نبود. لطفا به صورت دستی وارد شوید.", "بستن");
                                captcha.Generate();
                            }
                            else
                            {
                                var Token = "Token " + result2.Content.Token;
                                AppSettings.AccountSettings.ClientAuthorized = true;
                                AppSettings.AccountSettings.ClientToken = Token;
                                AppSettings.AccountInfo.ID = result2.Content.UserId;

                                APIManager.Resources.Authorization = Token;
                                //captcha.Generate();
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