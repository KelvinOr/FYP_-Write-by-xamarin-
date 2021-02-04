using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ViewMyDetailPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        FirebaseHelperII firebaseHelperII = new FirebaseHelperII();
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");

        private UserAccountDetail userAccountDetail { get; set; }

        public ViewMyDetailPage()
        {
            InitializeComponent();
            GetUserAccountDetails();

            //get update signal
            MessagingCenter.Subscribe<ViewMyDetailPage>(this, "refresh", (sender) =>
            {
                CheckUpdate();
            });

            if(Device.OS == TargetPlatform.Android)
            {
                androidBarH.IsVisible = false;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var getShopItem = await firebaseHelper.GetShopItemWithEmail(Preferences.Get("email", "").ToString());
                var getPost = await firebaseHelperII.GetPostbyEmail(Preferences.Get("email", "").ToString());
                lv_Item.Header = userAccountDetail;
                lv_Item.ItemsSource = getShopItem;
                lv_Post.Header = userAccountDetail;
                lv_Post.ItemsSource = getPost;
            }
            catch
            {}
        }

        private async void CheckUpdate()
        {
            bool isUpdate = false;
            int count = 0;
            while(isUpdate == false && count < 10)
            {
                var GetAccount = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
                if(GetAccount != null)
                {
                    var username = GetAccount.Object.UserName.ToString();
                    var imgsource = GetAccount.Object.UserImageURL.ToString();
                    var info = GetAccount.Object.UserInformation.ToString();
                    await Task.Delay(1000);
                    if (userAccountDetail.UserName != username || userAccountDetail.UserImageURL != imgsource || userAccountDetail.UserImageURL != info)
                    {
                        await Task.Delay(3000);
                        GetUserAccountDetails();
                        Debug.WriteLine("check one time");
                        isUpdate = true;
                    }
                    else
                    {
                        Debug.WriteLine("check one time");
                        count++;
                    }
                } 
            }
        }

        //update signal
        public void Sended()
        {
            MessagingCenter.Send<ViewMyDetailPage>(this, "refresh");
        }

        //back button action
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
 
        public async void GetUserAccountDetails()
        {
            var GetAccount = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

            if (GetAccount != null)
            {
                userAccountDetail = GetAccount.Object;
            }
        }

        private async void btn_setting_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AccountManagerPage());
        }

        private async void lv_Item_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
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

        private void btn_Post_Clicked(System.Object sender, System.EventArgs e)
        {
            lv_Item.IsEnabled = false;
            lv_Item.IsVisible = false;
            lv_Post.IsVisible = true;
            lv_Post.IsEnabled = true;
        }

        private void btn_Item_Clicked(System.Object sender, System.EventArgs e)
        {
            lv_Item.IsEnabled = true;
            lv_Item.IsVisible = true;
            lv_Post.IsVisible = false;
            lv_Post.IsEnabled = false;
        }

        private async void btn_search_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}
