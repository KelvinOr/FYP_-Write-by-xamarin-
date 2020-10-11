using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        async private void Save_Clicked(object sender, EventArgs e)
        {

            FirebaseHelper firebaseHelper = new FirebaseHelper();
            firebaseHelper.UpdateUserName(UserName.Text.ToString());
            GetUserAccountDetails();

        }


        public async void GetUserAccountDetails()
        {
            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            if (GetAccount != null)
            {
                lb_user.Text = GetAccount.Object.UserName.ToString();
            }
            else
            {
                lb_user.Text = "null";
            }
        }

    }


}