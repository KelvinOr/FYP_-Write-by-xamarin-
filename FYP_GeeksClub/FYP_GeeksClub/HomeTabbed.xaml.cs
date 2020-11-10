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
        readonly Page SecondHandShopPage;

        public HomeTabbed()
        {   
            InitializeComponent();

            this.BarBackgroundColor = Color.FromHex("#1C83F8");

            HomePage = new HomePage() {Title = "Home", IconImageSource = "Home24px.png"};
            AccountManagerPage = new AccountManagerPage() {Title = "Account", IconImageSource = "User24px.png" };
            SecondHandShopPage = new SecondHandShopPage() { Title = "SecondHand Item Shop" };
            

            Children.Add(HomePage);
            Children.Add(SecondHandShopPage);
            Children.Add(AccountManagerPage);

        }


        
    }
}