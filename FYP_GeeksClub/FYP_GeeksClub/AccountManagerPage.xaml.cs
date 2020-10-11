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
    public partial class AccountManagerPage : ContentPage
    {
        
        public AccountManagerPage()
        {
            InitializeComponent();
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
    }


}