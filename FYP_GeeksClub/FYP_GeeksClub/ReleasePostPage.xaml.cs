using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<imgStr> img = new ObservableCollection<imgStr>();
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
                    img.Add(new imgStr() { imgStream = image, stream = file.GetStream() });
                    lv_img.ItemsSource = img;
                }
            }
            catch { }
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

        public class imgStr
        {
            public ImageSource imgStream { get; set; }
            public Stream stream { get; set; }
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
