﻿using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountManagerPage : ContentPage
    {
        public bool i = false ;
        ViewMyDetailPage viewMyDetailPage = new ViewMyDetailPage();

        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public AccountManagerPage()
        {
            InitializeComponent();   
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();

        }

        async private void ChangeName_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Change Name", "Input new name");
            if (result != null)
            {
                firebaseHelper.UpdateUserName(result);
            }
            //GetUserAccountDetails();   
        }

        async private void ChangeImage_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new SelectImagePage());

        }

        async private void EditInformation_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Change Information", "Input change information");
            if (result != null)
            {
                firebaseHelper.UpdateUserInformation(result);
                viewMyDetailPage.Sended();
            }
            
        }

        async private void Download_Clicked(object sender, EventArgs e)
        {
            var Getfile = await firebaseHelper.GetUesrImage(Preferences.Get("email", "").ToString());
            //imgSorce.Source = Getfile.ToString();
        }

        async private void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            await Navigation.PushAsync(new SelectLogin());
        }

        async private void MyItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserReleasedItemPage());
        }

    }

       


    }
