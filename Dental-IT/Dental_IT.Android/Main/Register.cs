using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Fragments;
using Dental_IT.Droid.Adapters;
using Dental_IT.Model;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Java.Util.Regex;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Register : Android.Support.V7.App.AppCompatActivity
    {
        private EditText register_NameField;
        private EditText register_EmailField;
        private EditText register_PasswordField;
        private EditText register_RepeatPasswordField;
        private EditText register_DOBField;
        private Spinner register_GenderSpinner;
        private EditText register_NRICField;
        private EditText register_MobileField;
        private CheckBox register_PdpaChkbox;
        private TextView register_PdpaText;
        private Button register_RegisterBtn;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to register layout
            SetContentView(Resource.Layout.Register);

            //  Create widgets
            register_NameField = FindViewById<EditText>(Resource.Id.register_NameField);
            register_EmailField = FindViewById<EditText>(Resource.Id.register_EmailField);
            register_PasswordField = FindViewById<EditText>(Resource.Id.register_PasswordField);
            register_RepeatPasswordField = FindViewById<EditText>(Resource.Id.register_RepeatPasswordField);
            register_DOBField = FindViewById<EditText>(Resource.Id.register_DOBField);
            register_GenderSpinner = FindViewById<Spinner>(Resource.Id.register_GenderSpinner);
            register_NRICField = FindViewById<EditText>(Resource.Id.register_NRICField);
            register_MobileField = FindViewById<EditText>(Resource.Id.register_MobileField);
            register_PdpaChkbox = FindViewById<CheckBox>(Resource.Id.register_PdpaChkbox);
            register_PdpaText = FindViewById<TextView>(Resource.Id.register_PdpaText);
            register_RegisterBtn = FindViewById<Button>(Resource.Id.register_RegisterBtn);

            EditText[] fields = { register_NameField, register_EmailField, register_PasswordField, register_RepeatPasswordField, register_NRICField, register_MobileField };

            RunOnUiThread(() =>
            {
                //  Set textfield text sizes to be same as button text sizes
                register_NameField.SetTextSize(Android.Util.ComplexUnitType.Px, register_RegisterBtn.TextSize);
                register_EmailField.SetTextSize(Android.Util.ComplexUnitType.Px, register_RegisterBtn.TextSize);
                register_PasswordField.SetTextSize(Android.Util.ComplexUnitType.Px, register_RegisterBtn.TextSize);
                register_RepeatPasswordField.SetTextSize(Android.Util.ComplexUnitType.Px, register_RegisterBtn.TextSize);
                register_DOBField.SetTextSize(Android.Util.ComplexUnitType.Px, register_RegisterBtn.TextSize);
                register_NRICField.SetTextSize(Android.Util.ComplexUnitType.Px, register_RegisterBtn.TextSize);
                register_MobileField.SetTextSize(Android.Util.ComplexUnitType.Px, register_RegisterBtn.TextSize);

                //  Set DOB onClick to show and implement DatePicker
                register_DOBField.Click += delegate
                {
                    SelectDate(register_DOBField);

                    if (register_DOBField.Error != null)
                    {
                        register_DOBField.Error = null;
                    }
                };

                //  Configure spinner adapter for gender dropdown
                register_GenderSpinner.Adapter = new SpinnerAdapter(this, genders, true);

                //  Configure pdpa dialog
                IClickableSpan pdpaClick = new IClickableSpan();

                Android.Text.SpannableString pdpaSpan = new Android.Text.SpannableString(register_PdpaText.Text);
                pdpaSpan.SetSpan(pdpaClick, 16, 36, Android.Text.SpanTypes.ExclusiveExclusive);
                pdpaSpan.SetSpan(new Android.Text.Style.ForegroundColorSpan(new Android.Graphics.Color(Resource.Color.dark_blue)), 16, 36, Android.Text.SpanTypes.ExclusiveExclusive);
                register_PdpaText.TextFormatted = pdpaSpan;
                register_PdpaText.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;

                pdpaClick.Click += delegate
                {
                    AlertDialog.Builder pdpa = new AlertDialog.Builder(this);
                    pdpa.SetTitle(Resource.String.pdpa_title);
                    pdpa.SetMessage(Resource.String.pdpa_text);
                    pdpa.SetNeutralButton(Resource.String.close, delegate
                    {
                        pdpa.Dispose();
                    });

                    var alert = pdpa.Show();

                    TextView alertMessage = (TextView)alert.Window.DecorView.FindViewById(Android.Resource.Id.Message);
                    alertMessage.SetTextSize(Android.Util.ComplexUnitType.Px, Convert.ToSingle(register_RegisterBtn.TextSize * 0.8));
                };

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.register_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });

            //  Handle form submit
            register_RegisterBtn.Click += delegate
            {
                //  Validate fields
                if (Validate(fields))
                {
                    //  Close keyboard
                    InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                    //  Create new user
                    User user = new User()
                    {
                        UserName = register_EmailField.Text,
                        Name = register_NameField.Text,
                        Email = register_EmailField.Text,
                        Password = register_PasswordField.Text,
                        ConfirmPassword = register_RepeatPasswordField.Text,
                        DOB = register_DOBField.Text,
                        Gender = register_GenderSpinner.SelectedItem.ToString(),
                        NRIC = register_NRICField.Text,
                        Mobile = register_MobileField.Text
                    };

                    //  Send create request to database
                    switch (api.PostUserAccount(Newtonsoft.Json.JsonConvert.SerializeObject(user)))
                    {
                        //  Successful
                        case 1:
                            Toast.MakeText(this, GetString(Resource.String.account_created), ToastLength.Short).Show();
                            Intent intent = new Intent(this, typeof(Sign_In));
                            StartActivity(intent);
                            break;

                        //  Account exists
                        case 2:
                            Toast.MakeText(this, Resource.String.account_taken, ToastLength.Short).Show();
                            break;

                        //  No internet connectivity
                        case 3:
                            Toast.MakeText(this, Resource.String.network_error, ToastLength.Short).Show();
                            break;

                        //  Backend problem
                        case 4:
                            Toast.MakeText(this, Resource.String.register_error, ToastLength.Short).Show();
                            break;
                    }
                }
            };
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }
        
        //  Redirect to sign in page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Sign_In));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }

        //  Method to call DatePickerFragment
        private void SelectDate(EditText register_DOBField)
        {
            DatePickerFragment fragment = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                register_DOBField.Text = time.ToString("d MMMM yyyy");
            });

            fragment.Show(FragmentManager, "DatePickerFragment");
        }

        //  List of genders to populate spinner adapter
        private List<string> genders = new List<string>()
        {
            "Gender",
            "Male",
            "Female"
        };

        private bool Validate(EditText[] fields)
        {
            //  Check for empty fields
            foreach (EditText field in fields)
            {
                if (field.Text.Trim().Length == 0)
                {
                    field.RequestFocus();
                    field.Error = GetString(Resource.String.required_field);

                    return false;
                }
                else
                {
                    if (field == register_EmailField)
                    {
                        //  Check if email is in correct format
                        if (Android.Util.Patterns.EmailAddress.Matcher(register_EmailField.Text).Matches() == false)
                        {
                            field.RequestFocus();
                            field.Error = GetString(Resource.String.invalid_email);

                            return false;
                        }
                    }

                    if (field == register_PasswordField)
                    {
                        //  Check if password meets requirements
                        Java.Util.Regex.Pattern pattern;
                        Matcher matcher;

                        string PASSWORD_PATTERN = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&+=])(?=\\S+$).{6,}$";

                        pattern = Java.Util.Regex.Pattern.Compile(PASSWORD_PATTERN);
                        matcher = pattern.Matcher(field.Text);

                        bool match =  matcher.Matches();

                        if (match == false)
                        {
                            field.RequestFocus();
                            field.Error = GetString(Resource.String.invalid_password);

                            return false;
                        }
                    }
                    
                    if (field == register_RepeatPasswordField)
                    {
                        //  Check if repeat password is the same as password
                        if (field.Text.Equals(register_PasswordField.Text) == false)
                        {
                            field.RequestFocus();
                            field.Error = GetString(Resource.String.invalid_repeat);

                            return false;
                        }
                    }

                    if (field == register_MobileField)
                    {
                        //  Check if mobile number is invalid
                        if (field.Text.Length != 8)
                        {
                            register_MobileField.RequestFocus();
                            register_MobileField.Error = GetString(Resource.String.invalid_mobile);

                            return false;
                        }
                    }
                }
            }            

            //  Check if DOB is not selected
            if (register_DOBField.Text.Length == 0)
            {
                TextView errorText = (TextView)register_DOBField;
                errorText.Hint = GetString(Resource.String.no_date);
                errorText.SetHintTextColor(new Android.Graphics.Color(GetColor(Resource.Color.red)));
                errorText.Error = "";

                return false;
            }

            //  Check if gender is not selected
            if (register_GenderSpinner.SelectedItem.ToString().Equals("Gender"))
            {
                TextView errorText = (TextView)register_GenderSpinner.SelectedView;
                errorText.Text = GetString(Resource.String.no_gender);
                errorText.SetTextColor(new Android.Graphics.Color(GetColor(Resource.Color.red)));
                errorText.Error = "";

                return false;
            }

            //  Check if pdpa is not checked
            if (register_PdpaChkbox.Checked == false)
            {
                AlertDialog.Builder pdpaCheck = new AlertDialog.Builder(this);
                pdpaCheck.SetMessage(Resource.String.pdpa_unchecked);
                pdpaCheck.SetNeutralButton(Resource.String.OK, delegate
                {
                    pdpaCheck.Dispose();
                });
                pdpaCheck.Show();

                return false;
            }

            return true;
        }
    }

    //  Class for implementing clickable span
    class IClickableSpan : Android.Text.Style.ClickableSpan
    {
        public Action<View> Click;

        public override void OnClick(View widget)
        {
            Click?.Invoke(widget);
        }
    }
}