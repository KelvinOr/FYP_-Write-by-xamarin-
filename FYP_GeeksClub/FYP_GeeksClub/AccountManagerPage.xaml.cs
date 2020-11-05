using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountManagerPage : ContentPage
    {


        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        MediaFile file;


        public AccountManagerPage()
        {
            InitializeComponent();
            
            GetUserAccountDetails();

        }

        protected override bool OnBackButtonPressed()
        {
            var tabbedPage = this.Parent as TabbedPage;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    tabbedPage.CurrentPage = new HomePage();
                }
            });
            return true;
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

        }

        async private void ChangeName_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Change Name", "Input new name");
            if (result != null)
            {
                firebaseHelper.UpdateUserName(result);
            }
            GetUserAccountDetails();   
        }


        public async void GetUserAccountDetails()
        {
            await Task.Delay(2000);
            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            if (GetAccount != null)
            {
                lb_user.Text = GetAccount.Object.UserName.ToString();
                await Task.Delay(2000);
                imgChoosed.Source = GetAccount.Object.UserImageURL.ToString();
            }
            else
            {
                lb_user.Text = "null";
                imgChoosed.Source = "https://firebasestorage.googleapis.com/v0/b/hareware-59ccb.appspot.com/o/UserImage%2Fdfimg.png?alt=media&token=754dea27-f78a-44ae-b08f-339fcc126618";
            }
        }

        async private void ChangeImage_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new SelectImagePage());

        }

        async private void Download_Clicked(object sender, EventArgs e)
        {
            var Getfile = await firebaseHelper.GetUesrImage(Preferences.Get("email", "").ToString());
            imgChoosed.Source = Getfile.ToString();
        }

        async private void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            await Navigation.PushAsync(new SelectLogin());
        }


    }

       


    }
