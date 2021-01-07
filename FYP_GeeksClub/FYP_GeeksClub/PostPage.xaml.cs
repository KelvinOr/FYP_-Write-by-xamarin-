using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class PostPage : ContentPage
    {
        FirebaseHelperII firebasehelperII = new FirebaseHelperII();

        public PostPage()
        {
            InitializeComponent();
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
        }

        private async void btn_setting_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AccountManagerPage());
        }

        void btn_release_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
