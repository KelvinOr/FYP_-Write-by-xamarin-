using Firebase.Database;
using FYP_GeeksClub.firebaseHelper;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectImagePage : ContentPage
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        MediaFile file;

        public SelectImagePage()
        {
            InitializeComponent();
            choosefile();
        }

        async void choosefile()
        {
            await Task.Delay(1000);
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
                    SelectImage.IsVisible = false;
                    ImageSorce.Source = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });
                }
            }
            catch (Exception ex)
            { }

        }

        async private void save_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UploadUserImage(file.GetStream(), Preferences.Get("email", "").ToString());
            var Getfile = await firebaseHelper.GetUesrImage(Preferences.Get("email", "").ToString());
            firebaseHelper.UpdateUserImage(Getfile);
            AccountManagerPage accountManagerPage = new AccountManagerPage();
            accountManagerPage.Sended();
            await Navigation.PopModalAsync();
        }

        async private void cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void SelectImage_OnClicked(object sender, EventArgs e)
        {
            choosefile();
        }
    }
}