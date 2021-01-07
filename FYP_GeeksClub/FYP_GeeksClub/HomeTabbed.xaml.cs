using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeTabbed : TabbedPage
    { 
        public HomeTabbed()
        {   
            InitializeComponent();

            this.BarBackgroundColor = Color.FromHex("#1C83F8");

            Children.Add(new HomePage() { Title = "Home", IconImageSource = "Home24px.png" });
            Children.Add(new PostPage() { Title = "Coummity", IconImageSource = "community24px.png"});
            Children.Add(new ShopPage() { Title = "Shop", IconImageSource = "shop_24px.png" });
            Children.Add(new ViewMyDetailPage() { Title = "Account", IconImageSource = "User24px.png" });

        }

    }
}