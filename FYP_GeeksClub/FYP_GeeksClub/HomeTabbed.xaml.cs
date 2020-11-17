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
    public partial class HomeTabbed : TabbedPage
    {
        readonly Page HomePage;
        readonly Page AccountManagerPage;
        readonly Page ShopPage;

        public HomeTabbed()
        {   
            InitializeComponent();

            this.BarBackgroundColor = Color.FromHex("#1C83F8");

            HomePage = new HomePage() {Title = "Home", IconImageSource = "Home24px.png"};
            AccountManagerPage = new AccountManagerPage() {Title = "Account", IconImageSource = "User24px.png" };
            ShopPage = new ShopPage() { Title = "Shop" };
            

            Children.Add(HomePage);
            Children.Add(ShopPage);
            Children.Add(AccountManagerPage);

        }


        
    }
}