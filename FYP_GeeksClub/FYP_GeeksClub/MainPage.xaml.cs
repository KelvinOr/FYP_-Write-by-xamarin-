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
            
            if (Preferences.ContainsKey("email") == false || Preferences.ContainsKey("password") == false)
            {
                await Task.Delay(1000);
                await Navigation.PushAsync(new SelectLogin());
            } else
            {
                try
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                    var email = Preferences.Get("email", "").ToString();
                    var password = Preferences.Get("password", "").ToString();
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                    var content = await auth.GetFreshAuthAsync();
                    var serializedcontnet = JsonConvert.SerializeObject(content);
                    Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                    await Task.Delay(1000);
                    await Navigation.PushAsync(new HomeTabbed());
                } catch (Exception ex)
                {
                    Preferences.Clear();
                    await Navigation.PushAsync(new SelectLogin());
                }
                
            }
        }
    }
}
