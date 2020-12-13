using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class OrderDetailPage : ContentPage
    {
        public string imageURL;
        public string title;
        public string price;

        public OrderDetailPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();

            imageURL = shopItemDetail.imageURL;
            title = shopItemDetail.title;
            price = shopItemDetail.price.ToString();

            img_sH.IsVisible = shopItemDetail.saleIng;
            img_Image.Source = shopItemDetail.imageURL;
            lb_title.Text = title;
            lb_price.Text = price;
            
        }
    }
}
