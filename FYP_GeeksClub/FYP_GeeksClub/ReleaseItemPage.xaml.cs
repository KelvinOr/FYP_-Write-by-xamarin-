using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReleaseItemPage : ContentPage
    {
        MediaFile file;

        public ReleaseItemPage()
        {
            InitializeComponent();
        }

        async private void choosefile()
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
                    btn_selectiamge.IsVisible = false;
                    SelectImage.Source = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });
                    SelectImage.HeightRequest = 230;
                }
            } catch { }

        }

        async private void btn_cencal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void btn_selectiamge_Clicked(object sender, EventArgs e)
        {
            choosefile();
        }
    }
}