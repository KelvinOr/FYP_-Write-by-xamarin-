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
using System;

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

        //not function(waiting for fix)
        /*public async Task<string> GetUserName(string email)
        {
            var GetAccount = (await firebaseClient.Child("UserAccountDetail")
                .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == email).FirstOrDefault();
            await Task.Delay(2000);
            return GetAccount.Object.UserName;
        }*/

        //MARK: shop item firebase helper
        public async void PushNewItem(string title, string detail, double price, int quantity, string imageURL, bool isSecondHand, bool saleIng)
        {
            await firebaseClient.Child("shopitem").PostAsync(new ShopItemDetail
            {
                title = title,
                detail = detail,
                price = price,
                quantity = quantity,
                imageURL = imageURL,
                isSecondHand = isSecondHand,
                saleIng = saleIng,
                owner = Preferences.Get("email", "").ToString(),
                time = DateTime.Now.ToString("yyyyMMddHHmmssffff")
            });
        }

        public async Task<bool> CheckItemIsRelease(string title)
        {
            var Check = (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(
                a => (a.Object.title == title) && (a.Object.owner == Preferences.Get("email", "").ToString())).FirstOrDefault();
            if(Check == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ShopItemDetail>> GetShopItem()
        {
            return (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(a => a.Object.saleIng != false).Select(item => new ShopItemDetail
            {
                title = item.Object.title,
                detail = item.Object.detail,
                price = item.Object.price,
                quantity = item.Object.quantity,
                imageURL = item.Object.imageURL,
                isSecondHand = item.Object.isSecondHand,
                saleIng = item.Object.saleIng,
                owner = item.Object.owner,
                time = item.Object.time
            }).OrderByDescending(o => o.time).ToList();
        }

        public async Task<List<ShopItemDetail>> GetShopItemWithEmail()
        {
            return (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(a => a.Object.owner == Preferences.Get("email","").ToString()).Select(item => new ShopItemDetail
            {
                title = item.Object.title,
                detail = item.Object.detail,
                price = item.Object.price,
                quantity = item.Object.quantity,
                imageURL = item.Object.imageURL,
                isSecondHand = item.Object.isSecondHand,
                saleIng = item.Object.saleIng,
                owner = item.Object.owner,
                time = item.Object.time
            }).OrderByDescending(o => o.time).ToList();
        }

        public async Task<string> UploadShopItemImage(Stream fileStream,string title)
        {
            var fileName = (title);
            var imageURL = await firebaseStorage.Child("ShopItemImage").Child(fileName).PutAsync(fileStream);
            return imageURL;
        }

        public async Task<string> GetItemImageURL(string title) {
            var fileName = (title);
            var imageURL = await firebaseStorage.Child("ShopItemImage").Child(fileName).GetDownloadUrlAsync();
            return imageURL;
        }

        public async void UpdateItem(string title, string detail,string owner ,double price, int quantity, string imageURL, bool isSecondHand, bool saleIng)
        {
            var Check = (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(
                a => (a.Object.title == title) && (a.Object.owner == owner) ).FirstOrDefault();
            if (Check != null)
            {
                var Update = (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(
                    a => a.Object.title == title).Where(b => b.Object.owner == owner).FirstOrDefault();
                await firebaseClient.Child("shopitem").Child(Update.Key).PutAsync(new ShopItemDetail()
                {
                    title = title,
                    detail = detail,
                    price = price,
                    quantity = quantity,
                    imageURL = imageURL,
                    isSecondHand = isSecondHand,
                    saleIng = saleIng,
                    owner = owner,
                    time = DateTime.Now.ToString("yyyyMMddHHmmssffff")
                });
            }
            else
            {
                await firebaseClient.Child("shopitem").PostAsync(new ShopItemDetail()
                {
                    title = title,
                    detail = detail,
                    price = price,
                    quantity = quantity,
                    imageURL = imageURL,
                    isSecondHand = isSecondHand,
                    saleIng = saleIng,
                    owner = owner,
                    time = DateTime.Now.ToString("yyyyMMddHHmmssffff")
                });
            }
        }

        public async void DeleteItemOldImage(string fileName)
        {

            await firebaseStorage.Child("ShopItemImage").Child(fileName).DeleteAsync();
        }

        public async void DeleteItem(string title)
        {
            var owner = Preferences.Get("email", "").ToString();
            var Update = (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(
                    a => a.Object.title == title).Where(b => b.Object.owner == owner).FirstOrDefault();
            await firebaseClient.Child("shopitem").Child(Update.Key).DeleteAsync();
        }

    }
}

