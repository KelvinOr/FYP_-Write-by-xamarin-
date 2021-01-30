using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Database;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ReleasePostPage : ContentPage
    {
        public ObservableCollection<PostViewModel> img = new ObservableCollection<PostViewModel>();
        MediaFile file;
        FirebaseHelperII firebaseHelper = new FirebaseHelperII();

        String username;
        String userImageURL;

        public ReleasePostPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lv_img.ItemsSource = img;
        }

        private async void btn_addImage_Clicked(System.Object sender, System.EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    return;
                }
                else
                {
                    ImageSource image = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });
                    img.Add(new PostViewModel() { imgStream = image, stream = file.GetStream() });                    
                    lv_img.ItemsSource = img;
                    btn_viewImage.Text = img.Count() + " Image";
                }
            }
            catch { }
        }

        private void btn_viewImg_Clicked(System.Object sender, System.EventArgs e)
        {
            contectlayout.IsVisible = false;
            imagelayout.IsVisible = true;
        }
        private void btn_viewcontect_Clicked(System.Object sender, System.EventArgs e)
        {
            contectlayout.IsVisible = true;
            imagelayout.IsVisible = false;
        }
        private async void btn_cencal_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void btn_release_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
            int maxID = 1;
            string firstImage = null;
            bool haveImg = false;
            if (ed_contect != null)
            {
                try
                {
                    var temp = await firebaseHelper.getAllPost();
                    maxID = temp.Max(a => a.id) + 1;
                }
                catch
                { maxID = 1; }

                if (file != null && img != null)
                {
                    for (var i = 0; i < img.Count(); i++)
                    {
                        var temp = img[i].stream;
                        string imageURL = await firebaseHelper.UploadPostImage(temp, Preferences.Get("email", ""), maxID, i);
                        firebaseHelper.UploadPostImageURL(maxID, imageURL);
                    }
                    firstImage = await firebaseHelper.getFirstImage(maxID);
                    haveImg = true;
                } else
                {
                    firstImage = null;
                    haveImg = false; 
                }
                getUserName();
                await Task.Delay(2000);
                firebaseHelper.PustPost(maxID, ed_contect.Text, Preferences.Get("email", ""), username, userImageURL, firstImage, haveImg);
            }

        }

        public async void getUserName()
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
            var GetAccount = (await firebaseClient.Child("UserAccountDetail").OnceAsync<UserAccountDetail>()).Where(a => a.Object.Email == Preferences.Get("email", "").ToString()).FirstOrDefault();
            if (GetAccount != null)
            {
                username = GetAccount.Object.UserName;
                userImageURL = GetAccount.Object.UserImageURL;
            }
        }


    }
   
}
