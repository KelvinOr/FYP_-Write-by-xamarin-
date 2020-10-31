using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.Form;
using System.Linq;
using Xamarin.Essentials;
using Firebase.Storage;
using System.Threading.Tasks;
using System.IO;

namespace FYP_GeeksClub.firebaseHelper
{
    public class FirebaseHelper
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("hareware-59ccb.appspot.com");

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

        public async Task<string> UploadUserImage(Stream fileStream, string fileName)
        {
            var imageURL = await firebaseStorage.Child("UserImage").Child(fileName).PutAsync(fileStream);
            return imageURL;
        }

        public async Task<string> GetUesrImage(string fileName)
        {
            return await firebaseStorage.Child("UserImage").Child(fileName).GetDownloadUrlAsync();
        }

        public async Task DeleteImage(string fileName)
        {
            await firebaseStorage.Child("UserImage").Child(fileName).DeleteAsync();

        }

    }
}
