using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ViewPostPage : ContentPage
    {
        FirebaseHelperII firebaseHelperII = new FirebaseHelperII();
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");

        private PostDetail postDetail { get; set; }

        public ViewPostPage(PostDetail post)
        {
            InitializeComponent();
            getuserAccount(post.PostOwner);
            postDetail = post;
            lv_cont.Header = postDetail;
        }

        private async void getuserAccount(string email)
        {
            var GetAccount =  (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == email).FirstOrDefault();
            if(GetAccount != null)
            {
                ownerImage.Source = GetAccount.Object.UserImageURL.ToString();
                lb_ownername.Text = GetAccount.Object.UserName.ToString();
            }
            
        }

        private async void Navigation_bar_Clicked(object sender, EventArgs e)
        {
            var get = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == postDetail.PostOwner).FirstOrDefault();
            List<UserAccountDetail> userAccountDetail = new List<UserAccountDetail>();
            userAccountDetail.Add(new UserAccountDetail() { Email = get.Object.Email, UserImageURL = get.Object.UserImageURL, UserInformation = get.Object.UserInformation, UserName = get.Object.UserName});

            await Navigation.PushAsync(new UserDetailPage(userAccountDetail.FirstOrDefault()));
        }


    }
}
