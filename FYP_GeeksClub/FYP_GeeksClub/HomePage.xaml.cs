using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FYP_GeeksClub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private FirebaseHelperII firebaseHelperII = new FirebaseHelperII();

        private List<PostDetail> post = new List<PostDetail>();
        private List<ShopItemDetail> item  = new List<ShopItemDetail>();
        private List<PostDetail> Viewpost = new List<PostDetail>();
        private List<ShopItemDetail> Viewitem = new List<ShopItemDetail>();

        public HomePage()
        {
            InitializeComponent();
            Preferences.Remove("First_Time_Login");
            if (Preferences.ContainsKey("First_Time_Login"))
            {
                return;
            }
            else
            {
                Preferences.Set("First_Time_Login", "true");
            }
            Preferences.Set("UseCase", "null");
            if (Preferences.Get("First_Time_Login","") != "true")
            {
                alert();
                Preferences.Remove("First_Time_Login");
                Preferences.Set("First_Time_Login","false");
            }

            refe.RefreshCommand = new Command(() => {
                refresh();
                refe.IsRefreshing = false;
            });

            getOrderCount();
        }

        private async void getOrderCount()
        {
            int temp = -1;
            while (temp == null || temp == -1)
            {
                var temp2 = await firebaseHelper.GetOrder();
                temp = temp2.Where(a => a.TranIsAccp == false).ToList().Count();
            }
            lb_unaccept_order.Text = "You get " + temp + " order";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            item = await firebaseHelper.GetShopItem();
            post = await firebaseHelperII.getAllPost();
            if (post != null && item != null)
            {
                recommand();
            }

        }

        private async void refresh()
        {
            item = await firebaseHelper.GetShopItem();
            post = await firebaseHelperII.getAllPost();
            if (post != null && item != null)
            {
                recommand();
            }
            getOrderCount();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Alert!", "Do you really want to exit the application?", "Yes", "No");
                if (result)
                {
                    if (Device.OS == TargetPlatform.Android)
                    {
                        System.Environment.Exit(0);
                    }
                }
            });
            return true;
        }

        private void recommand()
        {
            var key =  new RecommandKey().GetKey();
            var ran = new Random();
            string[] Type = { "CPU", "GPU", "Harddisk", "MotherBoard", "Power Supply", "RAM" }; 

            if (post != null)
            {
                var temp = "";
                for (int i = 0; i < 5; i++)
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
            

            if (item != null)
            {
                var temp = "";
                for (int i = 0; i < 5; i++)
                {
                    int index = ran.Next(key.Count());
                    int typeindex = ran.Next(Type.Count());
                    if (key[index] == temp)
                    {
                        Viewitem.Add(item[ran.Next(item.Count())]);
                        temp = key[index];
                        continue;
                    }
                    if (key[index] == Type[typeindex])
                    {
                        if (item.Where(a => a.itemType.ToLower().Contains(key[index].ToLower())).FirstOrDefault() != null)
                        {
                            Viewitem.Add(item.Where(a => a.itemType.ToLower().Contains(key[index].ToLower())).FirstOrDefault());
                        }
                        else
                        {
                            Viewitem.Add(item[ran.Next(item.Count())]);
                        }
                        temp = key[index];
                        continue;
                    }

                    if (item.Where(a => a.title.ToLower().Contains(key[index].ToLower())).FirstOrDefault() != null)
                    {
                        Viewitem.Add(item.Where(a => a.title.ToLower().Contains(key[index].ToLower())).FirstOrDefault());
                        temp = key[index];
                    }
                    else
                    {
                        Viewitem.Add(item[ran.Next(item.Count())]);
                        temp = key[index];
                    }
                }
            }
            

            cv_post.ItemsSource = Viewpost;
            cv_item.ItemsSource = Viewitem;
            Debug.Write(Viewitem.Count());
        }


        private async void btn_setting_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AccountManagerPage());
        }

        private async void btn_search_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void alert()
        {
            bool answer = await DisplayAlert("Alert", "Would you want to know what specification you need?", "Yes", "No");
            if (answer == true)
            {
                await Navigation.PushModalAsync(new GetUserUseCase());
            }
        }

        private async void ShopItem_Tapped(System.Object sender, System.EventArgs e)
        {
            var content =  sender as StackLayout;
            var model = content.BindingContext as ShopItemDetail;
            var get = Viewitem.Where(a => a.id == model.id).FirstOrDefault();

            await Navigation.PushAsync(new ShopItemPage(get));
        }

        private async void PostItem_Tapped(System.Object sender, System.EventArgs e)
        {
            var content = sender as StackLayout;
            var model = content.BindingContext as PostDetail;
            var get = Viewpost.Where(a => a.id == model.id).FirstOrDefault();

            await Navigation.PushAsync(new ViewPostPage(get));
        }

        private async void Order_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new OrderListPage());
        }
    }
}