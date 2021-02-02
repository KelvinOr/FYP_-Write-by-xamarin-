using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class ViewAllPostImage : ContentPage
    {
        FirebaseHelperII firebaseHelperII = new FirebaseHelperII();
        int id;

        public ViewAllPostImage(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        protected async override void OnAppearing() {
            lv_img.ItemsSource = await firebaseHelperII.getAllPostImg(id);
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
