using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Dental_IT.Droid
{
    [Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class Sign_In : Activity
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
            SetContentView (Resource.Layout.Sign_In);

            //  Create widgets
            EditText signIn_EmailField = FindViewById<EditText>(Resource.Id.signIn_EmailField);
            EditText signIn_PasswordField = FindViewById<EditText>(Resource.Id.signIn_PasswordField);
            Button signIn_SignInBtn = FindViewById<Button>(Resource.Id.signIn_SignInBtn);
            CheckBox signIn_RememberMeChkbox = FindViewById<CheckBox>(Resource.Id.signIn_RememberMeChkbox);
            TextView signIn_RegisterText = FindViewById<TextView>(Resource.Id.signIn_RegisterText);

            RunOnUiThread(() =>
            {
                //  Set button text size to be same as text field text sizes
                signIn_SignInBtn.SetTextSize(Android.Util.ComplexUnitType.Px, signIn_EmailField.TextSize);
            });

            //  Intent to redirect to main menu page
            signIn_SignInBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Main_Menu));
                StartActivity(intent);
            };

            //  Intent to redirect to register page  
            signIn_RegisterText.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Register));
                StartActivity(intent);
            };
        }
    }
}


