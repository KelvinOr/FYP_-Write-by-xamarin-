using FYP_GeeksClub.firebaseHelper;
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
        FirebaseHelper firebaseHelper = new FirebaseHelper();

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
                    SelectImage.HeightRequest = 250;
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

        private void btn_release_Clicked(object sender, EventArgs e)
        {

            if(Ent_Title != null && Ent_Detail != null && Ent_Price !=null && SelectImage.Source != null)
            {
                firebaseHelper.PushNewItem(Ent_Title.Text.ToString(), Ent_Detail.Text.ToString(), Convert.ToDouble(Ent_Price.Text.ToString()), Convert.ToInt32(Ent_quantity.Text.ToString()),"null" ,sw_isSecondHand.IsToggled, false );
            }
        }

    }
}