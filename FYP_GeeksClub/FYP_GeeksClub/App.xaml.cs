using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    public partial class App : Application
    {

        

        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(new APIKey().SyncRes);
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                
                BarBackgroundColor = Color.FromHex("#1C83E8")
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
