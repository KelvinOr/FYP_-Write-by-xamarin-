using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ReleasePostPage : ContentPage
    {
        List<Stream> img = new List<Stream>();
        MediaFile file;

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
                    img.Add(file.GetStream());
                    lv_img.ItemsSource = img;
                }
            }
            catch { }
        }

        private async void btn_cencal_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void btn_release_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
