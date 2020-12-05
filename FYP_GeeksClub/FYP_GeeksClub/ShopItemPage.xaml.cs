using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ShopItemPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");

        String owner = "";
        String Image = "";
        String title = "";
        String detail = "";
        String price = "";
        String quantity = "";

        public ShopItemPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();
            owner = shopItemDetail.owner.ToString();
            Image = shopItemDetail.imageURL.ToString();
            title = shopItemDetail.title.ToString();
            detail = shopItemDetail.detail.ToString();
            price = shopItemDetail.price.ToString();
            quantity = shopItemDetail.quantity.ToString();

            getItemDetail();
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
            firebaseHelper.UpdateItem(
                lb_title.Text,
                lb_detail.Text,
                owner,
                Convert.ToDouble(lb_price.Text),
                Convert.ToInt32(lb_quantity.Text),
                img_ItemImage.Source.ToString(),
                false,
                false);
        }
    }
}
