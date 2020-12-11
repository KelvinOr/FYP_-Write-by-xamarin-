using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
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

        async void ShopItem_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Binding binding = new Binding();
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }

            var content = e.SelectedItem as ShopItemDetail;

            await Navigation.PushAsync(new EditItemPage(content));

            ((ListView)sender).SelectedItem = null;
        }
    }
}
