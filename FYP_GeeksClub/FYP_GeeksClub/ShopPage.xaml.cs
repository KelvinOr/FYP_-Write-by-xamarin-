﻿using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        List<ShopItemDetail> shopitem = new List<ShopItemDetail>();

        public ShopPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                androidBarH.IsVisible = false;
            }

            ShopItem.RefreshCommand = new Command(() => {
                RefreshData();
                ShopItem.IsRefreshing = false;
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try { 
                shopitem = await firebaseHelper.GetShopItem();
                ShopItem.ItemsSource = shopitem;
            } catch
            {
                ShopItem.IsVisible = false;
                haveItemLB.IsVisible = true;
            }
            
        }

        protected override bool OnBackButtonPressed()
        {
            var tabbedPage = this.Parent as TabbedPage;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    tabbedPage.CurrentPage = new HomePage();
                }
            });
            return true;
        }


        async private void btn_release_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ReleaseItemPage());   
        }

        private async void btn_setting_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AccountManagerPage());
        }

        async void ShopItem_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
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

        public async void RefreshData()
        {
            shopitem = await firebaseHelper.GetShopItem();
            ShopItem.ItemsSource = shopitem;
        }

        private async void btn_search_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
