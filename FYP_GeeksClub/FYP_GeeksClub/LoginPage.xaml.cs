using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public string WebAPIkey = "AIzaSyAIFwIiakmB2aCvW6BEKhPheokVAYTgjGc";
       

        public LoginPage()
        {
            InitializeComponent();
        }

        async private void btn_Login_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(ent_Email.Text, ent_Password.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                Preferences.Set("email", ent_Email.Text);
                Preferences.Set("password", ent_Password.Text);
                await Navigation.PushAsync(new HomeTabbed());

            }
            catch (Exception ex)
            {
                await Task.Delay(200);
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }

            

        }

        private async void Btn_back_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void btn_changepas_Clicked(System.Object sender, System.EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
            string result = await DisplayPromptAsync("Forget Password", "Input your email");
            if (result != null)
            {
                try
                {
                    await authProvider.SendPasswordResetEmailAsync(ent_Email.Text);
                }
                catch
                {

                }
            }
        }
    }
}