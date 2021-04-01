using System;
using System.Collections.Generic;
using System.Linq;
using FYP_GeeksClub.firebaseHelper;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public partial class MoreRItem : ContentPage
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private List<ShopItemDetail> item = new List<ShopItemDetail>();
        private List<ShopItemDetail> Viewitem = new List<ShopItemDetail>();

        public MoreRItem()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            item = await firebaseHelper.GetShopItem();
            if (item != null)
            {
                recommand();
            }

        }


        private void recommand()
        {
            var key = new RecommandKey().GetKey();
            var ran = new Random();
            string[] Type = { "CPU", "GPU", "Harddisk", "MotherBoard", "Power Supply", "RAM" };

            if (item != null)
            {
                var temp = "";
                for (int i = 0; i < 20; i++)
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
            ShopItem.ItemsSource = Viewitem;
        }

        async void ShopItem_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Binding binding = new Binding();
            if (((ListView)sender).SelectedItem == null)
            {
                return;
            }

            var content = e.SelectedItem as ShopItemDetail;

            await Navigation.PushAsync(new ShopItemPage(content));

            ((ListView)sender).SelectedItem = null;
        }

    }
}
