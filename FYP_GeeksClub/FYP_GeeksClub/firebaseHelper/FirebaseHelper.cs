using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.Form;
using System.Linq;
using Xamarin.Essentials;
using Firebase.Storage;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FYP_GeeksClub.firebaseHelper
{
    public class FirebaseHelper
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("hareware-59ccb.appspot.com");

        //MARK: user account firebase helper
        public async void UpdateUserName(string Username)
        {
            var Check = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
               a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
            var UserImageURL = GetAccount.Object.UserImageURL.ToString();
      
            if (Check == null)
            {
                await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL 
                }); ;
            }
            else
            {
                var Update = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
                    a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
                await firebaseClient.Child("UserAccountDetail").Child(Update.Key).PutAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL
                });
            }

        }

        public async void UpdateUserImage(string UserImageURL)
        {
            var Check = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
               a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
            var Username = GetAccount.Object.UserName.ToString();

            if (Check == null)
            {
                await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL
                }); ;
            }
            else
            {
                var Update = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
                    a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
                await firebaseClient.Child("UserAccountDetail").Child(Update.Key).PutAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL
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
            var imageURL = await firebaseStorage.Child("UserImage").Child(fileName).GetDownloadUrlAsync();
            return imageURL;
        }

        public async Task DeleteImage(string fileName)
        {
            await firebaseStorage.Child("UserImage").Child(fileName).DeleteAsync();

        }

        public async Task<List<UserAccountDetail>> GetAllUser()
        {
            return (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Select(item => new UserAccountDetail
            {
                UserName = item.Object.UserName,
                Email = item.Object.Email,
                UserImageURL = item.Object.UserImageURL
            }).ToList();
        }

        //MARK: shop item firebase helper
        public async void PushNewItem(string title, string detail, double price, string imageURL, bool isSecondHand, bool isSaled)
        {
            await firebaseClient.Child("shopitem").PatchAsync(new ShopItemDetail
            {
                title = title,
                detail = detail,
                price = price,
                imageURL = imageURL,
                isSecondHand = isSecondHand,
                isSaled = isSaled,
                owner = Preferences.Get("email", "").ToString(),
            });
        }

        public async Task<List<ShopItemDetail>> GetShopItem()
        {
            return (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Select(item => new ShopItemDetail
            {
                title = item.Object.title,
                detail = item.Object.detail,
                price = item.Object.price,
                imageURL = item.Object.imageURL,
                isSecondHand = item.Object.isSecondHand,
                isSaled = item.Object.isSaled,
                owner = item.Object.owner,
            }).ToList();

        }

    }
}
