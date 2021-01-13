using System;
using System.Collections.Generic;
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
                Task.Delay(5000);
                GetUserAccountDetails();
            });
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
            var GetAccount = (await firebaseClient
                  .Child("UserAccountDetail")
                  .OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();

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
                img_userImage.Source = "https://firebasestorage.googleapis.com/v0/b/hareware-59ccb.appspot.com/o/UserImage%2Fdfimg.png?alt=media&token=754dea27-f78a-44ae-b08f-339fcc126618";
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
