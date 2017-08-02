using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;

namespace Dental_IT.Droid.Main
{
    [Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class Sign_In : Activity
	{
        private EditText signIn_EmailField;
        private EditText signIn_PasswordField;
        private Button signIn_SignInBtn;
        private CheckBox signIn_RememberMeChkbox;
        private TextView signIn_RegisterText;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            //  Check if user should be auto signed in
            if (prefs.Contains("remembered"))
            {
                if (prefs.GetBoolean("remembered", false) == true)
                {
                    Intent intent = new Intent(this, typeof(Main_Menu));
                    StartActivity(intent);
                }
            }

            //  Hide title label, prevent content resizing with status bar
            RequestWindowFeature(WindowFeatures.NoTitle);

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)
                    (SystemUiFlags.LayoutStable |
                    SystemUiFlags.LayoutFullscreen);

            //  Set view to login layout
            SetContentView (Resource.Layout.Sign_In);

            //  Create widgets
            signIn_EmailField = FindViewById<EditText>(Resource.Id.signIn_EmailField);
            signIn_PasswordField = FindViewById<EditText>(Resource.Id.signIn_PasswordField);
            signIn_SignInBtn = FindViewById<Button>(Resource.Id.signIn_SignInBtn);
            signIn_RememberMeChkbox = FindViewById<CheckBox>(Resource.Id.signIn_RememberMeChkbox);
            signIn_RegisterText = FindViewById<TextView>(Resource.Id.signIn_RegisterText);

            RunOnUiThread(() =>
            {
                //  Set button text size to be same as text field text sizes
                signIn_SignInBtn.SetTextSize(Android.Util.ComplexUnitType.Px, signIn_EmailField.TextSize);
            });

            //  Handle form submit
            signIn_SignInBtn.Click += delegate
            {
                //  Validate fields
                bool validated = Validate();

                if (validated == true)
                {
                    //  Save user session (remember me) if checkbox is selected
                    if (signIn_RememberMeChkbox.Checked == true)
                    {
                        editor.PutBoolean("remembered", true);
                        editor.Apply();
                    }

                    Intent intent = new Intent(this, typeof(Main_Menu));
                    StartActivity(intent);
                }
            };

            //  Intent to redirect to register page  
            signIn_RegisterText.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Register));
                StartActivity(intent);
            };
        }

        private bool Validate()
        {
            if (signIn_EmailField.Text.Trim().Length == 0)
            {
                signIn_EmailField.RequestFocus();
                signIn_EmailField.Error = GetString(Resource.String.require_email);

                return false;
            }

            if (signIn_PasswordField.Text.Trim().Length == 0)
            {
                signIn_PasswordField.RequestFocus();
                signIn_PasswordField.Error = GetString(Resource.String.require_password);

                return false;
            }

            return true;
        }
    }
}


