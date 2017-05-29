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

            //  Hide title label, prevent content resizing with status bar
            RequestWindowFeature(WindowFeatures.NoTitle);

            this.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
                    (SystemUiFlags.LayoutStable |
                    SystemUiFlags.LayoutFullscreen);

            //  Set view to login layout
            SetContentView (Resource.Layout.Login);

            //  Create widgets
            EditText emailField = FindViewById<EditText>(Resource.Id.emailField);
            EditText passwordField = FindViewById<EditText>(Resource.Id.passwordField);
            Button signInBtn = FindViewById<Button>(Resource.Id.signInBtn);
            CheckBox rememberMeCkhbox = FindViewById<CheckBox>(Resource.Id.rememberMeChkbox);
            TextView registerText = FindViewById<TextView>(Resource.Id.registerText);

            //  Set button text size to be same as text field text sizes
            signInBtn.SetTextSize(Android.Util.ComplexUnitType.Px, emailField.TextSize);

            //// Get our button from the layout resource,
            //// and attach an event to it
            //Button button = FindViewById<Button> (Resource.Id.myButton);

            //button.Click += delegate {
            //	button.Text = string.Format ("{0} clicks!", count++);
            //};
        }
    }
}


