using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Register : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Hide title label, prevent content resizing with status bar
            RequestWindowFeature(WindowFeatures.NoTitle);

            this.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
                    (SystemUiFlags.LayoutStable |
                    SystemUiFlags.LayoutFullscreen);

            //  Set view to register layout
            SetContentView(Resource.Layout.Register);

            
        }
    }
}