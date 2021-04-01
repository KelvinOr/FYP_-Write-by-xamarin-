using System;
using System.Collections.Generic;
using System.Linq;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class MoreRPost : ContentPage
    {
        private FirebaseHelperII firebaseHelperII = new FirebaseHelperII();
        private List<PostDetail> post = new List<PostDetail>();
        private List<PostDetail> Viewpost = new List<PostDetail>();

        public MoreRPost()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            post = await firebaseHelperII.getAllPost();
            if (post != null)
            {
                recommand();
            }

        }

        private void recommand()
        {
            var key = new RecommandKey().GetKey();
            var ran = new Random();
            string[] Type = { "CPU", "GPU", "Harddisk", "MotherBoard", "Power Supply", "RAM" };

            if (post != null)
            {
                var temp = "";
                for (int i = 0; i < 20; i++)
                {
                    int index = ran.Next(key.Count());
                    if (key[index] == temp)
                    {
                        Viewpost.Add(post[ran.Next(post.Count())]);
                        temp = key[index];
                    }
                    if (post.Where(a => a.PostContect.ToLower().Contains(key[index].ToLower())).FirstOrDefault() != null)
                    {
                        Viewpost.Add(post.Where(a => a.PostContect.ToLower().Contains(key[index].ToLower())).FirstOrDefault());
                        temp = key[index];
                    }
                    else
                    {
                        Viewpost.Add(post[ran.Next(post.Count())]);
                        temp = key[index];
                    }

                }
            }
            lv_Post.ItemsSource = Viewpost;
        }

        private async void lv_Post_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Binding binding = new Binding();
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }

            var content = e.SelectedItem as PostDetail;

            await Navigation.PushAsync(new ViewPostPage(content));

            ((ListView)sender).SelectedItem = null;
        }
    }
}
