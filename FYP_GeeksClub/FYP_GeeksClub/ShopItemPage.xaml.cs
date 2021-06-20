using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ShopItemPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        FirebaseClient firebaseClient = new FirebaseClient(new APIKey().FirebaseClient);

        String owner = "";
        String Image = "";
        String title = "";
        String detail = "";
        String price = "";
        String quantity = "";
        String imgSource = "";
        ShopItemDetail shop;

        public ShopItemPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();
            owner = shopItemDetail.owner.ToString();
            Image = shopItemDetail.imageURL.ToString();
            title = shopItemDetail.title.ToString();
            detail = shopItemDetail.detail.ToString();
            price = shopItemDetail.price.ToString();
            quantity = shopItemDetail.quantity.ToString();
            imgSource = shopItemDetail.imageURL;
            shop = shopItemDetail;

            getItemDetail();

            if(owner == Preferences.Get("email", "").ToString())
            {
                btn_update.Text = "Update Items";
            }
        }
           

        private async void getItemDetail()
        {
            
            img_ItemImage.Source = Image;
            lb_title.Text = title;
            lb_detail.Text = detail;
            lb_price.Text = price;
            lb_quantity.Text = quantity;
            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == owner).FirstOrDefault();
            lb_owner.Text = GetAccount.Object.UserName.ToString();
            img_userImage.Source = GetAccount.Object.UserImageURL.ToString();
        }
        

        private async void btn_update_Clicked(object sender, EventArgs e)
        {
            if (owner == Preferences.Get("email", "").ToString())
            {
                await Navigation.PushAsync(new EditItemPage(shop));
            }
            else
            {
                await Navigation.PushAsync(new OrderDetailPage(shop));
            }
            Navigation.RemovePage(this);
        }

        private async void btn_ownerPage_Clicked(object sender, EventArgs e)
        {
            var temp = await firebaseHelper.GetUserDetail(owner);
            await Navigation.PushAsync(new UserDetailPage(temp));
        }

    }
}
