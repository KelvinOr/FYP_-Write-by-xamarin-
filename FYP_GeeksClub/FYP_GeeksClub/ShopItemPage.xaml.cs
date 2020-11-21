using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ShopItemPage : ContentPage
    {
        public ShopItemPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();

            img_ItemImage.Source = shopItemDetail.imageURL.ToString();
            title.Text = shopItemDetail.title.ToString();
        }
    }
}
