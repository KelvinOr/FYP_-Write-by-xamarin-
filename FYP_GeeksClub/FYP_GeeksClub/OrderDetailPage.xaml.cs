﻿using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using FYP_GeeksClub.firebaseHelper;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Essentials;

namespace FYP_GeeksClub
{
    public partial class OrderDetailPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public int id;
        public string imageURL;
        public string title;
        public string itemType;
        public double price;
        public string itemOwnerEmail;
        public int quanity;
        public string detail;
        public bool isSecondHand;

        public OrderDetailPage(ShopItemDetail shopItemDetail)
        {
            InitializeComponent();

            //init
            id = shopItemDetail.id;
            imageURL = shopItemDetail.imageURL;
            title = shopItemDetail.title;
            price = shopItemDetail.price;
            itemOwnerEmail = shopItemDetail.owner;
            quanity = shopItemDetail.quantity;
            detail = shopItemDetail.detail;
            isSecondHand = shopItemDetail.isSecondHand;
            itemType = shopItemDetail.itemType;

            img_sH.IsVisible = shopItemDetail.saleIng;
            img_Image.Source = shopItemDetail.imageURL;
            
            lb_title.Text = title;
            lb_price.Text = price.ToString();
        }

        async private void btn_AddOrder_Clicked(System.Object sender, System.EventArgs e)
        {
            string other = "null";
            if (Ent_ContMeth.Text != null && Ent_Phone.Text != null)
            {
                if (Ent_Other.Text == null)
                {
                    other = "null";
                }
                else
                {
                    other = Ent_Other.Text.ToString();
                }
                bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Are you sure the detail is correct", "Yes", "No");

                var user = await firebaseHelper.GetUserDetail(Preferences.Get("email", "").ToString());

                if (answer == true)
                {
                    var temp = await firebaseHelper.GetShopItem();
                    int maxID = temp.Max(a => a.id);
                    firebaseHelper.NewOrder(maxID, id, title, imageURL, price, itemOwnerEmail, user.UserName ,Convert.ToInt32(Ent_Phone.Text.ToString()), user.UserImageURL ,Ent_ContMeth.Text.ToString(), other);
                    bool saling = false;
                    var int_quantity = quanity - 1;
                    if (int_quantity == 0)
                    {
                        saling = false;
                    }
                    else
                    {
                        saling = true;
                    }

                    firebaseHelper.UpdateItem(id, title, detail, itemOwnerEmail, price, int_quantity, imageURL, isSecondHand, saling, itemType);
                    await App.Current.MainPage.DisplayAlert("Alert", "Order Created", "OK");
                    await Navigation.PopAsync();
                };
                
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Input all detail", "OK");
            }
        }
    }
}
