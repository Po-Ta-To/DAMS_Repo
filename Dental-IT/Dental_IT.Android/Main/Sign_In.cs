﻿using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;
using Dental_IT.Model;
using Android.Views.InputMethods;

namespace Dental_IT.Droid.Main
{
    [Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class Sign_In : Activity
	{
        private RelativeLayout signIn_Layout;
        private EditText signIn_EmailField;
        private EditText signIn_PasswordField;
        private Button signIn_SignInBtn;
        private CheckBox signIn_RememberMeChkbox;
        private TextView signIn_RegisterText;

        API api = new API();

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
            signIn_Layout = FindViewById<RelativeLayout>(Resource.Id.signIn_Layout);
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
                    //  Close keyboard
                    InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    if (inputManager != null)
                    {
                        inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
                    }

                    //  Post credentials to get token from database
                    switch (api.PostUserForToken(signIn_EmailField.Text, signIn_PasswordField.Text))
                    {
                        //  Successful
                        case 1:
                            //  Save access token to shared preferences
                            editor.PutString("token", UserAccount.AccessToken);
                            editor.Apply();

                            //  Get user account with token
                            if (api.GetUserAccount(UserAccount.AccessToken) == true)
                            {
                                //  Save user session (remember me) if checkbox is selected
                                if (signIn_RememberMeChkbox.Checked == true)
                                {
                                    editor.PutBoolean("remembered", true);
                                    editor.Apply();
                                }

                                //  Save name and userID to shared preferences
                                editor.PutString("name", UserAccount.Name);
                                editor.PutInt("userID", UserAccount.ID);
                                editor.Apply();

                                //  Redirect to main menu
                                Intent intent = new Intent(this, typeof(Main_Menu));
                                StartActivity(intent);
                            }
                            else
                            {
                                Toast.MakeText(this, Resource.String.access_denied, ToastLength.Short).Show();
                            }
                            break;

                        //  Invalid credentials
                        case 2:
                            Toast.MakeText(this, Resource.String.invalid_signIn, ToastLength.Short).Show();
                            break;

                        //  No internet connectivity
                        case 3:
                            Toast.MakeText(this, Resource.String.network_error, ToastLength.Short).Show();
                            break;

                        //  Backend problem
                        case 4:
                            Toast.MakeText(this, Resource.String.server_error, ToastLength.Short).Show();
                            break;

                        default:
                            Toast.MakeText(this, Resource.String.error, ToastLength.Short).Show();
                            break;
                    }
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


