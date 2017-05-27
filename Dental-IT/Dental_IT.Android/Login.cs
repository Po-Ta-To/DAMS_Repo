using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Dental_IT.Droid
{
	[Activity(MainLauncher = true)]
	public class Login : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            this.RequestWindowFeature(WindowFeatures.NoTitle);            
            this.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.Fullscreen | SystemUiFlags.HideNavigation);

            // Set our view from the "login" layout resource
            SetContentView (Resource.Layout.Login);
                        

            //// Get our button from the layout resource,
            //// and attach an event to it
            //Button button = FindViewById<Button> (Resource.Id.myButton);

            //button.Click += delegate {
            //	button.Text = string.Format ("{0} clicks!", count++);
            //};
        }
	}
}


