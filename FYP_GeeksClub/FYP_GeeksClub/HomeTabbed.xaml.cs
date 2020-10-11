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
        readonly Page tab1Page;
        readonly Page tab2Page;

        public HomeTabbed()
        {   
            InitializeComponent();

            tab1Page = new HomePage() { Title = "Home", IconImageSource = "Home.png"};
            tab2Page = new AccountManagerPage() { Title = "User" , IconImageSource = "User.png"};

            Children.Add(tab1Page);
            Children.Add(tab2Page);

            
            
        }

        
    }
}