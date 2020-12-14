using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class OrderDetailPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public string imageURL;
        public string title;
        public double price;
        public string itemOwnerEmail;

        public OrderDetailPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();

            //init
            imageURL = shopItemDetail.imageURL;
            title = shopItemDetail.title;
            price = shopItemDetail.price;
            itemOwnerEmail = shopItemDetail.owner;

            img_sH.IsVisible = shopItemDetail.saleIng;
            img_Image.Source = shopItemDetail.imageURL;
            
            lb_title.Text = title;
            lb_price.Text = price.ToString();
        }

        private async void btn_AddOrder_Clicked(System.Object sender, System.EventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Are you sure the detail is correct", "Yes", "No");
            if(answer == true)
            {
                firebaseHelper.NewOrder(title, price, itemOwnerEmail, Convert.ToInt32(Ent_Phone.Text.ToString()), Ent_ContMeth.Text.ToString(), Ent_Other.Text.ToString());
            };
        }
    }
}
