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
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");

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
                lv_Item.ItemsSource = getShopItem;
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
                    if (lb_owner.Text != username || img_userImage.Source.ToString() != imgsource || lb_UserInfo.Text != info)
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
                lb_owner.Text = GetAccount.Object.UserName.ToString();
                img_userImage.Source = GetAccount.Object.UserImageURL.ToString();
                lb_UserInfo.Text = GetAccount.Object.UserInformation.ToString();
            }
            else
            {
                lb_owner.Text = "null";
                lb_UserInfo.Text = "null";
                img_userImage.Source = firebaseHelper.getDefImg();
            }
        }

        private async void btn_setting_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AccountManagerPage());
        }

        void lv_Item_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

        }

        private void btn_Post_Clicked(System.Object sender, System.EventArgs e)
        {
            btn_Item.BackgroundColor = Color.FromHex("#C4C4C4");
            btn_Post.BackgroundColor = Color.White;
            lv_Item.IsVisible = false;
        }

        private void btn_Item_Clicked(System.Object sender, System.EventArgs e)
        {
            btn_Item.BackgroundColor = Color.White;
            btn_Post.BackgroundColor = Color.FromHex("#C4C4C4");
            lv_Item.IsVisible = true;
        }
    }
}
