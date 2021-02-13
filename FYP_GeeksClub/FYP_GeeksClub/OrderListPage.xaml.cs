using System;
using System.Collections.Generic;
using System.Linq;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class OrderListPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        List<OrderDetail> order = new List<OrderDetail>();
        List<OrderDetail> order2 = new List<OrderDetail>();

        public OrderListPage()
        {
            InitializeComponent();
            pk_OrderICrea.SelectedIndex = 0;
            pk_OrderIGet.SelectedIndex = 0;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                order = await firebaseHelper.GetOrder();
                order2 = await firebaseHelper.GetOrderbyCust();
                lv_unaccept_order.ItemsSource = order.Where(o => o.TranIsAccp == false).ToList();
                lv_accept_order.ItemsSource = order.Where(o => o.TranIsAccp == true).ToList();
                lv_unaccept_create.ItemsSource = order2.Where(o => o.TranIsAccp == false).ToList();
                set_null();
                
            }
            catch { }
        }

        private async void Accept_Clicked(System.Object sender, System.EventArgs e)
        {
            var content = sender as Button;
            var model = content.BindingContext as OrderDetail;

            bool answer = await DisplayAlert("Alert", "Would you want to accept this transaction", "Yes", "No");
            if (answer == true)
            {
                firebaseHelper.UpdateOrder(model.id, model.ItemId, model.ItemImg, model.ItemTitle, model.ItemPrice, model.ItemOwner, model.CustName, model.CustPhone, model.CustImg, model.ContMethod, model.Other, true);
            }
        }

        private async void NoAccept_Clicked(System.Object sender, System.EventArgs e)
        {
            var content = sender as Button;
            var model = content.BindingContext as OrderDetail;

            bool answer = await DisplayAlert("Alert", "Would you want to delete this transaction", "Yes", "No");
            if (answer == true)
            {
                var item = await firebaseHelper.SearchItem(model.ItemId);
                item.quantity += 1;
                firebaseHelper.UpdateItem(item.id, item.title, item.detail, item.owner, item.price, item.quantity,item.imageURL,item.isSecondHand, item.saleIng);
            }
        }

        private void set_null()
        {
            if (lv_accept_order.IsVisible == true)
            {
                if (lv_accept_order.ItemsSource == null)
                {
                    OrderIGet_null.IsVisible = true;
                }
            }

            if (lv_unaccept_order.IsVisible == true)
            {
                if (lv_accept_order.ItemsSource == null)
                {
                    OrderIGet_null.IsVisible = true;
                }
            }

            if(lv_unaccept_create.IsVisible == true)
            {
                if(lv_unaccept_create.ItemsSource == null)
                {
                    OrderICrea_null.IsVisible = true;
                }
            }

        }

        private async void ViewDetail_Clicked(System.Object sender, System.EventArgs e)
        {
            var content = sender as Button;
            var model = content.BindingContext as OrderDetail;
            await DisplayAlert("Alert", "Name: " + model.CustName + "\nPhone No.: " + model.CustPhone + "\nEmail: " + model.CustEmail + "\nContect Method: " + model.ContMethod + "\nother: " + model.Other, "OK");
        }

        private void btn_OrderGet(System.Object sender, System.EventArgs e)
        {
            st_OrderIGet.IsVisible = true;
            st_OrderICreate.IsVisible = false;
            OrderGet.BackgroundColor = Color.White;
            OrderICreate.BackgroundColor = Color.FromHex("#C4C4C4");
            
        }

        private void btn_OrderICreate(System.Object sender, System.EventArgs e)
        {
            st_OrderIGet.IsVisible = false;
            st_OrderICreate.IsVisible = true;
            OrderGet.BackgroundColor = Color.FromHex("#C4C4C4");
            OrderICreate.BackgroundColor = Color.White;
        }

        private void pk_OrderIGet_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            switch (pk_OrderIGet.SelectedItem)
            {
                case ("Unaccetp Order"):
                    lv_unaccept_order.IsVisible = true;
                    lv_accept_order.IsVisible = false;
                    break;
                case ("Accetped Order"):
                    lv_unaccept_order.IsVisible = false;
                    lv_accept_order.IsVisible = true;
                    break;
                default:
                    break;
            }

            switch (pk_OrderICrea.SelectedItem)
            {
                case ("Unaccetp Order"):
                    lv_unaccept_create.IsVisible = true;
                    lv_accept_create.IsVisible = false;
                    break;
                case ("Accetped Order"):
                    lv_unaccept_create.IsVisible = false;
                    lv_accept_create.IsVisible = true;
                    break;
                default:
                    break;
            }


        }
    }
}
