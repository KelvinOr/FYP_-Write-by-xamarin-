using System;
using Xamarin.Forms;

namespace FYP_GeeksClub
{
    public class SuperListView: ListView
    {
        public static readonly BindableProperty IsScrollingEnableProperty =
        BindableProperty.Create(nameof(IsScrollingEnable),
                                typeof(bool),
                                typeof(SuperListView),
                                true);

        public bool IsScrollingEnable
        {
            get { return (bool)GetValue(IsScrollingEnableProperty); }
            set { SetValue(IsScrollingEnableProperty, value); }
        }
    }
}
