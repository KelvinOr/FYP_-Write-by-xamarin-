using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace FYP_GeeksClub.firebaseHelper
{
    public class FirebaseHelper
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");

        public async void UpdateUserName(string Username)
        {
            var Check = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
               a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            if (Check == null)
            {
                await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username
                }); ;
            }
            else
            {
                var Update = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
                    a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
                await firebaseClient.Child("UserAccountDetail").Child(Update.Key).PutAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username
                });
            }

        }


    }
}
