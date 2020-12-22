using System;
using System.Collections.Generic;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class UserDetailPage : ContentPage
    {
        public UserDetailPage(UserAccountDetail userAccountDetail)
        {
            InitializeComponent();
            email.Text = userAccountDetail.Email.ToString();
        }
    }
}
