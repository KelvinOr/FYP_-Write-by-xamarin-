using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xam.Plugins.OnDeviceCustomVision;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class EditItemPage : ContentPage
    {
        MediaFile file;
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public int id;
        public string imageURL;
        public string title;
        public string detail;
        public double price;
        public int quantity;
        public bool saleIng;
        public bool isSecondHand;

        Guid projectID = Guid.Parse("b11bca26-3803-44b1-8343-cfdd4a15142a");
        string ENDPOINT = "https://japaneast.api.cognitive.microsoft.com";
        string predictionKey = "71feb66e158e430a968977d7a3675e1d";

        public EditItemPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();
            id = shopItemDetail.id;
            imageURL = shopItemDetail.imageURL.ToString();
            title = shopItemDetail.title.ToString();
            detail = shopItemDetail.detail.ToString();
            price = shopItemDetail.price;
            quantity = shopItemDetail.quantity;
            saleIng = shopItemDetail.saleIng;
            isSecondHand = shopItemDetail.isSecondHand;
            Type.SelectedItem = shopItemDetail.itemType;

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
                }

                if (Device.RuntimePlatform == Device.iOS)
                {
                    var tags = await CrossImageClassifier.Current.ClassifyImage(file.GetStream());
                    var besttags = tags.OrderByDescending(t => t.Probability).First().Tag;
                    Type.SelectedItem = besttags;

                    if (besttags == null)
                    {
                        OnlineImageClasstif(file.GetStream());
                    }
                }


                if (Device.RuntimePlatform == Device.Android)
                {
                    OnlineImageClasstif(file.GetStream());
                }
            }
            catch { }

        }

        private async void OnlineImageClasstif(Stream stream)
        {
            var client = new CustomVisionPredictionClient
            {
                ApiKey = predictionKey,
                Endpoint = ENDPOINT,
            };

            var result = await client.ClassifyImageAsync(projectID, "Iteration4", stream);
            var bestResult = result.Predictions.OrderByDescending(p => p.Probability).FirstOrDefault();

            Ent_Title.Text = bestResult.TagName;
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
            //await Navigation.PopModalAsync();
            var filename = Preferences.Get("email", "").ToString() + id.ToString();
            if(file != null)
            {
                firebaseHelper.UploadShopItemImage(file.GetStream(), filename).ToString();
                await Task.Delay(2000);
                String imageURL = await firebaseHelper.GetItemImageURL(filename);
                firebaseHelper.UpdateItem(id, Ent_Title.Text.ToString(), Ent_Detail.Text.ToString(), Preferences.Get("email", "").ToString(), Convert.ToDouble(Ent_Price.Text.ToString()), Convert.ToInt32(Ent_quantity.Text.ToString()), imageURL, sw_isSecondHand.IsToggled, sw_saling.IsToggled, Type.SelectedItem.ToString());
            }
            else
            {
                String imageURL = this.imageURL;
                firebaseHelper.UpdateItem(id, Ent_Title.Text.ToString(), Ent_Detail.Text.ToString(), Preferences.Get("email", "").ToString(), Convert.ToDouble(Ent_Price.Text.ToString()), Convert.ToInt32(Ent_quantity.Text.ToString()), imageURL, sw_isSecondHand.IsToggled, sw_saling.IsToggled, Type.SelectedItem.ToString());
            }
            
            await Navigation.PopAsync();
        }
    }
}
