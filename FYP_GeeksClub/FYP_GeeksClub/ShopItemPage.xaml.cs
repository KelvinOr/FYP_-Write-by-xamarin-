using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ShopItemPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        
        

        public ShopItemPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();

            img_ItemImage.Source = shopItemDetail.imageURL.ToString();
            lb_title.Text = shopItemDetail.title.ToString();
            lb_detail.Text = shopItemDetail.detail.ToString();
            lb_owner.Text = shopItemDetail.owner.ToString();
            lb_price.Text = shopItemDetail.price.ToString();
            lb_quantity.Text = shopItemDetail.quantity.ToString();
        }

        private async void btn_update_Clicked(object sender, EventArgs e)
        {
            firebaseHelper.UpdateItem(
                lb_title.Text,
                lb_detail.Text,
                lb_owner.Text,
                Convert.ToDouble(lb_price.Text),
                Convert.ToInt32(lb_quantity.Text),
                img_ItemImage.Source.ToString(),
                false,
                false);
        }
    }
}
