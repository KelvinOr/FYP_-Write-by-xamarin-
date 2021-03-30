using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.Form;
using System.Linq;
using Xamarin.Essentials;
using Firebase.Storage;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System;

namespace FYP_GeeksClub.firebaseHelper
{
    public class FirebaseHelper
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("hareware-59ccb.appspot.com");

        public string getDefImg()
        {  
            return "https://firebasestorage.googleapis.com/v0/b/hareware-59ccb.appspot.com/o/defUserImg%2Fuser.png?alt=media&token=f78d728e-e2df-487b-847f-f7bf8a0c6ea0";
        }

        //MARK: user account firebase helper
        public async void UpdateUserName(string Username)
        {
            var Check = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
               a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
            var UserImageURL = GetAccount.Object.UserImageURL.ToString();
            var UserInfor = GetAccount.Object.UserInformation.ToString();
      
            if (Check == null)
            {
                await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL,
                    UserInformation = UserInfor
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
                    UserImageURL = UserImageURL,
                    UserInformation = UserInfor
                });
            }

        }
      
        public async void UpdateUserInformation(string UserInformation)
        {
            var Check = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
               a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            var UserImageURL = Check.Object.UserImageURL.ToString();
            var Username = Check.Object.UserName.ToString();

            if (Check == null)
            {
                await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL,
                    UserInformation = UserInformation
                });
            }
            else
            {
                var Update = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(
                    a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
                await firebaseClient.Child("UserAccountDetail").Child(Update.Key).PutAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL,
                    UserInformation = UserInformation
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
            var UserInfor = GetAccount.Object.UserInformation.ToString();

            if (Check == null)
            {
                await firebaseClient.Child("UserAccountDetail").PostAsync(new UserAccountDetail()
                {
                    Email = Preferences.Get("email", "").ToString(),
                    UserName = Username,
                    UserImageURL = UserImageURL,
                    UserInformation = UserInfor
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
                    UserImageURL = UserImageURL,
                    UserInformation = UserInfor
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
                UserImageURL = item.Object.UserImageURL,
                UserInformation = item.Object.UserInformation
            }).ToList();
        }

        public async Task<UserAccountDetail> GetUserDetail(string email)
        {
            return (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => (a.Object.Email == email)).Select(item => new UserAccountDetail
            {
                UserName = item.Object.UserName,
                Email = item.Object.Email,
                UserImageURL = item.Object.UserImageURL,
                UserInformation = item.Object.UserInformation
            }).FirstOrDefault();
        }

        //MARK: shop item firebase helper
        public async void PushNewItem(int id, string title, string detail, double price, int quantity, string imageURL, bool isSecondHand, bool saleIng, string itemType)
        {
            await firebaseClient.Child("shopitem").PostAsync(new ShopItemDetail
            {
                id = id,
                title = title,
                itemType = itemType,
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
                id = item.Object.id,
                title = item.Object.title,
                itemType = item.Object.itemType,
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

        public async Task<List<ShopItemDetail>> GetShopItemWithEmail(string email)
        {
            return (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(a => a.Object.owner == email).Select(item => new ShopItemDetail
            {
                id = item.Object.id,
                title = item.Object.title,
                itemType = item.Object.itemType,
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

        public async Task<ShopItemDetail> SearchItem(int id)
        {
            return (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(a => a.Object.id == id).Select(item => new ShopItemDetail
            {
                id = item.Object.id,
                title = item.Object.title,
                itemType =item.Object.itemType,
                detail = item.Object.detail,
                price = item.Object.price,
                quantity = item.Object.quantity,
                imageURL = item.Object.imageURL,
                isSecondHand = item.Object.isSecondHand,
                saleIng = item.Object.saleIng,
                owner = item.Object.owner,
                time = item.Object.time
            }).FirstOrDefault();
        }

        public async Task<string> UploadShopItemImage(Stream fileStream ,string title)
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

        public async void UpdateItem(int id, string title, string detail,string owner ,double price, int quantity, string imageURL, bool isSecondHand, bool saleIng, string itemType)
        {
            var Check = (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(
                a => (a.Object.title == title) && (a.Object.owner == owner) ).FirstOrDefault();
            if (Check != null)
            {
                var Update = (await firebaseClient.Child("shopitem").OnceAsync<ShopItemDetail>()).Where(
                    a => a.Object.title == title).Where(b => b.Object.owner == owner).FirstOrDefault();
                await firebaseClient.Child("shopitem").Child(Update.Key).PutAsync(new ShopItemDetail()
                {
                    id = id,
                    title = title,
                    itemType = itemType,
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
                    id = id,
                    title = title,
                    itemType = itemType,
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

        //MARK: OrderDetail
        public async void NewOrder(int id, int itemid, string itemTitle, string itemImg,double itemPrice, string itemOwner, string custName, int custPhone, string custImg,string contMeth, string other)
        {
            await firebaseClient.Child("Order").PostAsync(new OrderDetail()
            {
                id = id,
                CustEmail = Preferences.Get("email", "").ToString(),
                ItemId = itemid,
                ItemTitle = itemTitle,
                ItemImg = itemImg,
                ItemPrice = itemPrice,
                ItemOwner = itemOwner,
                CustName = custName,
                CustPhone = custPhone,
                CustImg = custImg,
                ContMethod = contMeth,
                TranIsAccp = false,
                Other = other,
                Time = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                ShowTime = DateTime.Now.ToString()
            });
        }

        public async void UpdateOrder(int id, int itemid ,string itemImg,string itemTitle, double itemPrice, string itemOwner, string custName, int custPhone, string custImg,string contMeth, string other, bool tranIsAccp)
        {
            var Check = (await firebaseClient.Child("Order").OnceAsync<OrderDetail>()).Where(a => a.Object.id == id).FirstOrDefault();
            if (Check != null)
            {
                var Update = (await firebaseClient.Child("Order").OnceAsync<OrderDetail>()).Where(a => a.Object.id == id).FirstOrDefault();
                await firebaseClient.Child("Order").Child(Update.Key).PutAsync(new OrderDetail()
                {
                    id = id,
                    CustEmail = Preferences.Get("email", "").ToString(),
                    ItemId = itemid,
                    ItemTitle = itemTitle,
                    ItemPrice = itemPrice,
                    ItemImg = itemImg,
                    ItemOwner = itemOwner,
                    CustName = custName,
                    CustPhone = custPhone,
                    CustImg = custImg,
                    ContMethod = contMeth,
                    TranIsAccp = tranIsAccp,
                    Other = other,
                    Time = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    ShowTime = DateTime.Now.ToString()
                });
            }
            else
            {
                await firebaseClient.Child("Order").PostAsync(new OrderDetail()
                {
                    id = id, 
                    CustEmail = Preferences.Get("email", "").ToString(),
                    CustName = custName,
                    ItemTitle = itemTitle,
                    ItemId = itemid,
                    ItemPrice = itemPrice,
                    ItemOwner = itemOwner,
                    ItemImg = itemImg,
                    CustPhone = custPhone,
                    ContMethod = contMeth,
                    CustImg = custImg,
                    TranIsAccp = tranIsAccp,
                    Other = other,
                    Time = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    ShowTime = DateTime.Now.ToString()
                });
            }
        }

        public async Task<List<OrderDetail>> GetOrder()
        {
            return (await firebaseClient.Child("Order").OnceAsync<OrderDetail>()).Where(a => a.Object.ItemOwner == Preferences.Get("email", "").ToString()).Select(item => new OrderDetail
            {
                id = item.Object.id,
                CustName = item.Object.CustName,
                CustEmail = item.Object.CustEmail,
                CustImg = item.Object.CustImg,
                CustPhone = item.Object.CustPhone,
                ContMethod = item.Object.ContMethod,
                ItemId = item.Object.ItemId,
                ItemTitle = item.Object.ItemTitle,
                ItemPrice = item.Object.ItemPrice,
                ItemImg = item.Object.ItemImg,
                ItemOwner = item.Object.ItemOwner,
                TranIsAccp = item.Object.TranIsAccp,
                Other = item.Object.Other,
                Time = item.Object.Time,
                ShowTime = item.Object.ShowTime
            }).OrderByDescending(o => o.Time).ToList();
        }

        public async Task<List<OrderDetail>> GetOrderbyCust()
        {
            return (await firebaseClient.Child("Order").OnceAsync<OrderDetail>()).Where(a => a.Object.CustEmail == Preferences.Get("email", "").ToString()).Select(item => new OrderDetail
            {
                id = item.Object.id,
                CustName = item.Object.CustName,
                CustEmail = item.Object.CustEmail,
                CustImg = item.Object.CustImg,
                CustPhone = item.Object.CustPhone,
                ContMethod = item.Object.ContMethod,
                ItemId = item.Object.ItemId,
                ItemTitle = item.Object.ItemTitle,
                ItemPrice = item.Object.ItemPrice,
                ItemImg = item.Object.ItemImg,
                ItemOwner = item.Object.ItemOwner,
                TranIsAccp = item.Object.TranIsAccp,
                Other = item.Object.Other,
                Time = item.Object.Time,
                ShowTime = item.Object.ShowTime
            }).OrderByDescending(o => o.Time).ToList();
        }

        public async void removeOrder(int id)
        {
            var Update = (await firebaseClient.Child("Order").OnceAsync<OrderDetail>()).Where(a => a.Object.id == id).FirstOrDefault();
            await firebaseClient.Child("Order").Child(Update.Key).DeleteAsync();
        }


    }
}

