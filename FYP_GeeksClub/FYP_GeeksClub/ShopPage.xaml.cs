using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
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
    public partial class ShopPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ShopPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try { 
                var getShopItem = await firebaseHelper.GetShopItem();
                ShopItem.ItemsSource = getShopItem;
            } catch
            {
                ShopItem.IsVisible = false;
                haveItemLB.IsVisible = true;
            }
            
        }

        protected override bool OnBackButtonPressed()
        {
            var tabbedPage = this.Parent as TabbedPage;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    tabbedPage.CurrentPage = new HomePage();
                }
            });
            return true;
        }


        async private void btn_release_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ReleaseItemPage());   
        }

        async void ShopItem_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Binding binding = new Binding();
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }
            
            var content = e.SelectedItem as ShopItemDetail;

            await Navigation.PushAsync(new ShopItemPage(content));

            ((ListView)sender).SelectedItem = null;
        }
    }

    
}
