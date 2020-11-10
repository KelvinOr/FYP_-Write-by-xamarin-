using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondHandShopPage : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public SecondHandShopPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var getAllUser = await firebaseHelper.GetAllUser();
            SecondHandShopList.ItemsSource = getAllUser;
        }
    }
}