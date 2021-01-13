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

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzgxNzM3QDMxMzgyZTM0MmUzMEU0NFZrdHArcmU0K2IzbnpkY3ZlcU5nZndidk9Ld255SEhNSU9QV0hMOWc9");
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
