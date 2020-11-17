using FYP_GeeksClub.firebaseHelper;
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
            try
            {
                var getShopItem = await firebaseHelper.GetShopItem();
                ShopItem.ItemsSource = getShopItem;
            } catch
            {
                ShopItem.IsVisible = false;
                haveItemLB.IsVisible = true;
            }
            
        }
    }
}