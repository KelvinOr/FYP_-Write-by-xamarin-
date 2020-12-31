using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {

        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        public string WebAPIkey = "AIzaSyAIFwIiakmB2aCvW6BEKhPheokVAYTgjGc";
        public string defImgURL = "https://firebasestorage.googleapis.com/v0/b/hareware-59ccb.appspot.com/o/UserImage%2Fdfimg.png?alt=media&token=754dea27-f78a-44ae-b08f-339fcc126618";

        public SignupPage()
        {
            InitializeComponent();
        }
        
        private async void Btn_back_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async private void btn_Signup_Clicked(object sender, EventArgs e)
        {
            if (ent_Email == null || ent_comPass == null || ent_Password == null)
            {
                await DisplayAlert("Alert", "Please input all detail", "OK");
            } else if (ent_Password.Text != ent_comPass.Text)
            {
                await DisplayAlert("Alert", "Password and Comfirm Password must be same", "OK");
            } else
            {
                try
                {
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(ent_Email.Text, ent_Password.Text);
                    string gettoken = auth.FirebaseToken;
                    await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                    {
                        Email = ent_Email.Text.ToString(),  
                        UserName = ent_Email.Text.ToString(),
                        UserImageURL = defImgURL,
                        UserInformation = "null"
                    });
                    await DisplayAlert("Alert", "Sign Up finish", "OK");
                    await Navigation.PushAsync(new LoginPage());
                }
                catch (Exception ex)
                {
                    await Task.Delay(2000);
                    await DisplayAlert("Alert", "Sign Up fail", "OK");
                }

                
            }

        }
    }
}