using FYP_GeeksClub.firebaseHelper;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugins.OnDeviceCustomVision;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReleaseItemPage : ContentPage
    {
        MediaFile file;
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        Guid projectID = Guid.Parse("b11bca26-3803-44b1-8343-cfdd4a15142a");
        string  ENDPOINT = "https://japaneast.api.cognitive.microsoft.com";
        string predictionKey = "71feb66e158e430a968977d7a3675e1d";


        public ReleaseItemPage()
        {
            InitializeComponent();
        }

        async private void choosefile()
        {
            btn_FRb.IsVisible = true;
            btn_selectiamge.IsVisible = false;
            SelectImage.Source = null;
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {
                    FRb.IsVisible = true;
                    btn_selectiamge.IsVisible = false;
                    return;
                }
                else
                {
                    SelectImage.Source = ImageSource.FromStream(() =>
                    {
                        return file.GetStream();
                    });
                    FRb.IsVisible = false;
                    btn_selectiamge.IsVisible = true;
                }

                var tags = await CrossImageClassifier.Current.ClassifyImage(file.GetStream());
                var besttags = tags.OrderByDescending(t => t.Probability).First().Tag;
                Ent_Detail.Text = besttags;

                if (besttags == null)
                {
                    var client = new CustomVisionPredictionClient
                    {
                        ApiKey = predictionKey,
                        Endpoint = ENDPOINT,
                    };

                    var result = await client.ClassifyImageAsync(projectID, "Iteration4", file.GetStream());
                    var bestResult = result.Predictions.OrderByDescending(p => p.Probability).FirstOrDefault();

                    Ent_Title.Text = bestResult.TagName;
                }
            }
            catch { }

        }

        async private void btn_cencal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void btn_selectiamge_Clicked(object sender, EventArgs e)
        {
            choosefile();
        }

        async private void btn_release_Clicked(object sender, EventArgs e)
        {
            bool Check = await firebaseHelper.CheckItemIsRelease(Ent_Title.Text.ToString());
            if(Check == false)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Item Release Already", "OK");
            }
            else
            {
                try
                {
                    if (Ent_Title != null && Ent_Detail != null && Ent_Price != null && SelectImage.Source != null && SelectImage.Source != null)
                    {
                        int maxID = 1;
                        try
                        {
                            var temp = await firebaseHelper.GetShopItem();
                            maxID = temp.Max(a => a.id) + 1;
                        }
                        catch
                        {
                            maxID = 1;
                        }
                        await Navigation.PopModalAsync();
                        string filename = Preferences.Get("email", "").ToString() + maxID.ToString();
                        firebaseHelper.UploadShopItemImage(file.GetStream(), filename).ToString();
                        await Task.Delay(2000);
                        String imageURL = await firebaseHelper.GetItemImageURL(filename);
                        firebaseHelper.PushNewItem(maxID, Ent_Title.Text.ToString(), Ent_Detail.Text.ToString(), Convert.ToDouble(Ent_Price.Text.ToString()), Convert.ToInt32(Ent_quantity.Text.ToString()), imageURL, sw_isSecondHand.IsToggled, true);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Input all detail", "OK");
                    }
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Input all detail", "OK");
                }
            }
                
        }

    }
}