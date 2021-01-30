using System;
using FYP_GeeksClub;
using FYP_GeeksClub.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SuperListView), typeof(SuperListViewRenderer))]
namespace FYP_GeeksClub.Droid
{
    public class SuperListViewRenderer: ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            var superListView = Element as SuperListView;
            if (superListView == null)
                return;
        }
    }
}
