﻿using System;
using System.Collections.Generic;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class UserDetailPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        private string email;

        public UserDetailPage(UserAccountDetail userAccountDetail)
        {
            InitializeComponent();
            lb_owner.Text = userAccountDetail.UserName.ToString();
            img_userImage.Source = userAccountDetail.UserImageURL.ToString();
            email = userAccountDetail.Email;
            if (userAccountDetail.UserInformation == null)
            {
                lb_UserInfo.Text = "null"; 
            }
            else
            {
                lb_UserInfo.Text = userAccountDetail.UserInformation.ToString();
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var getShopItem = await firebaseHelper.GetShopItemWithEmail(email);
                lv_Item.ItemsSource = getShopItem;
            }
            catch
            {
            }

        }

        private void btn_Item_Clicked(object sender, EventArgs e)
        {
            btn_Item.BackgroundColor = Color.White;
            btn_Post.BackgroundColor = Color.FromHex("#C4C4C4");
            lv_Item.IsVisible = true;
        }

        private void btn_Post_Clicked(object sender, EventArgs e)
        {
            btn_Item.BackgroundColor = Color.FromHex("#C4C4C4");
            btn_Post.BackgroundColor = Color.White;
            lv_Item.IsVisible = false;
        }

    }
}