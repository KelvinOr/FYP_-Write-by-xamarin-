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
                /*var Check = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
                 a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

                if (Check == null)
                {
                    await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                    {
                        Email = Preferences.Get("email", "").ToString(),
                        UserName = Preferences.Get("email", "").ToString(),
                    }); ;
                }*/

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
    }
}