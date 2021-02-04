using System;
using System.Collections.Generic;
using System.Linq;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class SearchPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        FirebaseHelperII firebaseHelperII = new FirebaseHelperII();

        private List<PostDetail> postDetail = new List<PostDetail>();
        private List<ShopItemDetail> shopDetail = new List<ShopItemDetail>();
        private List<UserAccountDetail> userDetail = new List<UserAccountDetail>();

        public SearchPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            postDetail = await firebaseHelperII.getAllPost();
            shopDetail = await firebaseHelper.GetShopItem();
            userDetail = await firebaseHelper.GetAllUser();
            lv_post_search.ItemsSource = postDetail;
            lv_shop_search.ItemsSource = shopDetail;
            lv_user_search.ItemsSource = userDetail;
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != null)
            {
                lv_post_search.ItemsSource = postDetail.Where(a => a.PostContect.ToLower().Contains(search.Text.ToString().ToLower())).ToList();
                lv_shop_search.ItemsSource = shopDetail.Where(a => a.title.ToLower().Contains(search.Text.ToString().ToLower())).ToList();
                lv_user_search.ItemsSource = userDetail.Where(a => a.UserName.ToLower().Contains(search.Text.ToString().ToLower())).ToList();
            }
            
        }

        private void btn_post_Clicked(System.Object sender, System.EventArgs e)
        {
            btn_post.BackgroundColor = Color.White;
            btn_shop.BackgroundColor = Color.FromHex("#C4C4C4");
            btn_user.BackgroundColor = Color.FromHex("#C4C4C4");
            lv_post_search.IsVisible = true;
            lv_shop_search.IsVisible = false;
            lv_user_search.IsVisible = false;
        }

        private void btn_shop_Clicked(System.Object sender, System.EventArgs e)
        {
            btn_shop.BackgroundColor = Color.White;
            btn_post.BackgroundColor = Color.FromHex("#C4C4C4");
            btn_user.BackgroundColor = Color.FromHex("#C4C4C4");
            lv_post_search.IsVisible = false;
            lv_shop_search.IsVisible = true;
            lv_user_search.IsVisible = false;
        }

        private void btn_user_Clicked(System.Object sender, System.EventArgs e)
        {
            btn_user.BackgroundColor = Color.White;
            btn_shop.BackgroundColor = Color.FromHex("#C4C4C4");
            btn_post.BackgroundColor = Color.FromHex("#C4C4C4");
            lv_post_search.IsVisible = false;
            lv_shop_search.IsVisible = false;
            lv_user_search.IsVisible = true;
        }

        private async void lv_post_search_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
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

        private async void lv_shop_search_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
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

        private async void lv_user_search_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Binding binding = new Binding();
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }
            var content = e.SelectedItem as UserAccountDetail;
            await Navigation.PushAsync(new UserDetailPage(content));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
