using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class UserReleasedItemPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public UserReleasedItemPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var getShopItem = await firebaseHelper.GetShopItemWithEmail();
                ShopItem.ItemsSource = getShopItem;
            }
            catch
            {
                ShopItem.IsVisible = false;
                haveItemLB.IsVisible = true;
            }

        }
    }
}
