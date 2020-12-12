using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class EditItemPage : ContentPage
    {
        MediaFile file;
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public string imageURL;
        public string title;
        public string detail;
        public double price;
        public int quantity;
        public bool saleIng;
        public bool isSecondHand;

        public EditItemPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();
            imageURL = shopItemDetail.imageURL.ToString();
            title = shopItemDetail.title.ToString();
            detail = shopItemDetail.title.ToString();
            price = shopItemDetail.price;
            quantity = shopItemDetail.quantity;
            saleIng = shopItemDetail.saleIng;
            isSecondHand = shopItemDetail.isSecondHand;

            SelectImage.Source = imageURL;
            Ent_Title.Text = title;
            Ent_Detail.Text = detail;
            Ent_Price.Text = price.ToString();
            Ent_quantity.Text = quantity.ToString();
            sw_isSecondHand.IsToggled = isSecondHand;
            sw_saling.IsToggled = saleIng;
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
                    SelectImage.Source = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });
                    SelectImage.HeightRequest = 200;
                }
            }
            catch { }

        }



        async private void btn_cencal_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btn_selectiamge_Clicked(System.Object sender, System.EventArgs e)
        {
            choosefile();
        }

        private async void btn_save_Clicked(System.Object sender, System.EventArgs e)
        {
            var filename = Preferences.Get("email", "").ToString() + title.ToString();
            firebaseHelper.DeleteItemOldImage(filename);
            firebaseHelper.DeleteItem(title);
            await Task.Delay(1000);
            filename = Preferences.Get("email", "").ToString() + Ent_Title.Text.ToString();
            firebaseHelper.UploadShopItemImage(file.GetStream(), filename).ToString();
            await Task.Delay(2000);
            String imageURL = await firebaseHelper.GetItemImageURL(filename);
            firebaseHelper.PushNewItem(Ent_Title.Text.ToString(), Ent_Detail.Text.ToString(), Convert.ToDouble(Ent_Price.Text.ToString()), Convert.ToInt32(Ent_quantity.Text.ToString()), imageURL, sw_isSecondHand.IsToggled, sw_saling.IsToggled);
            await Navigation.PopAsync();
        }
    }
}
