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
        public bool i = false ;

        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public AccountManagerPage()
        {
            InitializeComponent();   
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
                ViewMyDetailPage viewMyDetailPage = new ViewMyDetailPage();
                viewMyDetailPage.Sended();
            }
            //GetUserAccountDetails();   
        }

        async private void ChangeImage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SelectImagePage());
        }

        async private void EditInformation_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Change Information", "Input change information");
            if (result != null)
            {
                firebaseHelper.UpdateUserInformation(result);
                ViewMyDetailPage viewMyDetailPage = new ViewMyDetailPage();
                viewMyDetailPage.Sended();
            }
            
        }

        async private void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            await Navigation.PushAsync(new SelectLogin());
        }

        async private void MyItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserReleasedItemPage());
        }

        private async void UseCase_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new GetUserUseCase());
        }

        private async void ShowOrder_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new OrderListPage());
        }
    }

}
