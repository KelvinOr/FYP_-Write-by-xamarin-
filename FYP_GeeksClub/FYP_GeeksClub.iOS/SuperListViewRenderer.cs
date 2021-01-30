using System;
using FYP_GeeksClub;
using FYP_GeeksClub.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(SuperListView), typeof(SuperListViewRenderer))]

namespace FYP_GeeksClub.iOS
{
    public class SuperListViewRenderer: ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            var superListView = Element as SuperListView;
            if (superListView == null)
                return;


            Control.ScrollEnabled = superListView.IsScrollingEnable;
        }

    }
    
}
