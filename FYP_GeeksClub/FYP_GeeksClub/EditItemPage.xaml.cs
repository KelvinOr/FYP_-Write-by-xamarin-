using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class EditItemPage : ContentPage
    {
        public EditItemPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();

            SelectImage.Source = shopItemDetail.imageURL.ToString();
        }
    }
}
