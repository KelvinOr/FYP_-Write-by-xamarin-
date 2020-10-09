using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

        }
        
        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform == Device.Android){}
                DependencyService.Get<IAndroidMethods>().CloseApp();

            return base.OnBackButtonPressed();
        }

        public interface IAndroidMethods
        {
            void CloseApp();
        }
        
        private async void Signout_OnClicked(object sender, EventArgs e)
        {
            Preferences.Clear();
        }
    }
}