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
    public partial class SelectLogin : ContentPage
    {
        public SelectLogin()
        {
            InitializeComponent();
        }

        private async void Btn_LwEmail_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        private async void btn_SwEmail_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignupPage());
        }
    }
    
}