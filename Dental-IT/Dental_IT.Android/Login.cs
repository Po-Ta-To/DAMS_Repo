using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Dental_IT.Droid
{
	[Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class Login : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            //  Sticky immersive mode - Note: ALL statements are needed to achieve this mode (idk why)
            this.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
                    (SystemUiFlags.LayoutStable |
                    SystemUiFlags.LayoutHideNavigation |
                    SystemUiFlags.LayoutFullscreen |
                    SystemUiFlags.HideNavigation |
                    SystemUiFlags.Fullscreen |
                    SystemUiFlags.ImmersiveSticky);

            //  Set view to login layout
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


