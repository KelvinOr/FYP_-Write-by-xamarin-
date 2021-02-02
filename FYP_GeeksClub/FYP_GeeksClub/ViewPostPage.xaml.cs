using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ViewPostPage : ContentPage
    {

        FirebaseHelperII firebaseHelperII = new FirebaseHelperII();
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");

        private PostDetail postDetail { get; set; }
        private UserAccountDetail user { get; set; }

        public ViewPostPage(PostDetail post)
        {
            InitializeComponent();
            getPostuserAccount(post.PostOwner);
            getMyAccount();
            postDetail = post;
            lv_cont.Header = postDetail;

            lv_cont.RefreshCommand = new Command(() => {
                RefreshData();
                lv_cont.IsRefreshing = false;
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var getRePost = await firebaseHelperII.getRePost(postDetail.id);
            lv_cont.ItemsSource = getRePost;
        }

            private async void getPostuserAccount(string email)
        {
            var GetAccount =  (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == email).FirstOrDefault();
            if (GetAccount != null)
            {
                ownerImage.Source = GetAccount.Object.UserImageURL.ToString();
                lb_ownername.Text = GetAccount.Object.UserName.ToString();
            }
            
        }

        private async void getMyAccount()
        {
            var GetAccount = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
            if (GetAccount != null)
            {
                user = GetAccount.Object;
            }

        }

        private async void btn_send_Clicked(object sender, EventArgs e)
        {
            var id = postDetail.id;
            var username = user.UserName;
            var img = user.UserImageURL;
            if (ed_detail != null)
            {
               firebaseHelperII.UploadRePost(id, ed_detail.Text.ToString(), Preferences.Get("email", "").ToString(), username, img);
            }
        }

        private async void Navigation_bar_Clicked(object sender, EventArgs e)
        {
            var get = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == postDetail.PostOwner).FirstOrDefault();
            List<UserAccountDetail> userAccountDetail = new List<UserAccountDetail>();
            userAccountDetail.Add(new UserAccountDetail() { Email = get.Object.Email, UserImageURL = get.Object.UserImageURL, UserInformation = get.Object.UserInformation, UserName = get.Object.UserName});

            await Navigation.PushAsync(new UserDetailPage(userAccountDetail.FirstOrDefault()));
        }

        public async void RefreshData()
        {
            var getRePost = await firebaseHelperII.getRePost(postDetail.id);
            lv_cont.ItemsSource = getRePost;
        }

    }
}
