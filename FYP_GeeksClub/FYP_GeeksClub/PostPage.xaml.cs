using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class PostPage : ContentPage
    {
        FirebaseHelperII firebasehelperII = new FirebaseHelperII();

        public PostPage()
        {
            InitializeComponent();

            if (Device.OS == TargetPlatform.Android)
            {
                androidBarH.IsVisible = false;
            }

            lv_Post.RefreshCommand = new Command(() => {
                RefreshData();
                lv_Post.IsRefreshing = false;
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var getShopItem = await firebasehelperII.getAllPost();
                lv_Post.ItemsSource = getShopItem;
            }
            catch
            {
                lv_Post.IsVisible = false;
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

        private async void lv_Post_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Binding binding = new Binding();
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }

            var content = e.SelectedItem as PostDetail;

            await Navigation.PushAsync(new ViewPostPage(content));

            ((ListView)sender).SelectedItem = null;
        }

        private async void btn_setting_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AccountManagerPage());
        }

        private async void btn_release_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new ReleasePostPage());
        }

        public async void RefreshData()
        {
            var getShopItem = await firebasehelperII.getAllPost();
            lv_Post.ItemsSource = getShopItem;
        }

        private async void btn_search_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
