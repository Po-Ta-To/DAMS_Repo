using Android.App;
using Android.Content;
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

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
                    (SystemUiFlags.LayoutStable |
                    SystemUiFlags.LayoutFullscreen);

            //  Set view to login layout
            SetContentView (Resource.Layout.Login);

            //  Create widgets
            EditText signInEmailField = FindViewById<EditText>(Resource.Id.signInEmailField);
            EditText signInPasswordField = FindViewById<EditText>(Resource.Id.signInPasswordField);
            Button signInBtn = FindViewById<Button>(Resource.Id.signInBtn);
            CheckBox rememberMeCkhbox = FindViewById<CheckBox>(Resource.Id.rememberMeChkbox);
            TextView registerText = FindViewById<TextView>(Resource.Id.registerText);

            //  Set button text size to be same as text field text sizes
            signInBtn.SetTextSize(Android.Util.ComplexUnitType.Px, signInEmailField.TextSize);

            //  Intent to redirect to main menu page
            signInBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Select_Hospital));
                StartActivity(intent);
            };

            //  Intent to redirect to register page  
            registerText.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Register));
                StartActivity(intent);
            };
        }
    }
}


