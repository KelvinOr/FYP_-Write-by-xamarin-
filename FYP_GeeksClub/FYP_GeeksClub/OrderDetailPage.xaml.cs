using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class OrderDetailPage : ContentPage
    {
        public OrderDetailPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();
            test_lb.Text = shopItemDetail.price.ToString();
        }
    }
}
