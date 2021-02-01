using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class UserDetailPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        FirebaseHelperII firebaseHelperII = new FirebaseHelperII();

        private string email;
        private UserAccountDetail user { get; set; }

        public UserDetailPage(UserAccountDetail userAccountDetail)
        {
            InitializeComponent();

            email = userAccountDetail.Email;
            user = userAccountDetail;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var getShopItem = await firebaseHelper.GetShopItemWithEmail(email);
                lv_Item.ItemsSource = getShopItem;
                lv_Item.Header = user;
                var getPostItem = await firebaseHelperII.GetPostbyEmail(email);
                lv_Post.Header = user;
                lv_Post.ItemsSource = getPostItem;
            }
            catch{}
        }

        async private void lv_Item_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Binding binding = new Binding();
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }

            var content = e.SelectedItem as ShopItemDetail;
            await Navigation.PushAsync(new ShopItemPage(content));
            ((ListView)sender).SelectedItem = null;
        }

        private async void btn_Item_Clicked(object sender, EventArgs e)
        {
            lv_Item.IsVisible = true;
            lv_Item.IsEnabled = true;
            lv_Post.IsVisible = false;
            lv_Post.IsEnabled = false;
        }

        private async void btn_Post_Clicked(object sender, EventArgs e)
        {

            lv_Post.IsVisible = true;
            lv_Post.IsEnabled = true;
            lv_Item.IsVisible = false;
            lv_Item.IsEnabled = false;
            
        }
    }
}
