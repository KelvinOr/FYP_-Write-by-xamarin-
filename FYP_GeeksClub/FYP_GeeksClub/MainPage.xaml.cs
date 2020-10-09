using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class MainPage : ContentPage
    {
        
        public string WebAPIkey = "AIzaSyAIFwIiakmB2aCvW6BEKhPheokVAYTgjGc";
        
        public MainPage()
        {
            InitializeComponent();
            
            IsLogin();
        }

        private async void IsLogin()
        {
            if (Application.Current.Properties.ContainsKey("email") ||
                Application.Current.Properties.ContainsKey("password"))
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var email = Application.Current.Properties["email"] as string;
                var password = Application.Current.Properties["password"] as string;
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await Navigation.PushAsync(new SelectLogin());
            }
        }
    }
}
