using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Fragments;
using Dental_IT.Droid.Adapters;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Register : Activity
    {
        private string TAG = "DatePickerFragment";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to register layout
            SetContentView(Resource.Layout.Register);

            //  Create widgets
            EditText register_EmailField = FindViewById<EditText>(Resource.Id.register_EmailField);
            EditText register_PasswordField = FindViewById<EditText>(Resource.Id.register_PasswordField);
            EditText register_RepeatPasswordField = FindViewById<EditText>(Resource.Id.register_RepeatPasswordField);
            EditText register_DOBField = FindViewById<EditText>(Resource.Id.register_DOBField);
            Spinner register_GenderSpinner = FindViewById<Spinner>(Resource.Id.register_GenderSpinner);
            EditText register_NRICField = FindViewById<EditText>(Resource.Id.register_NRICField);
            EditText register_MobileField = FindViewById<EditText>(Resource.Id.register_MobileField);
            CheckBox register_PdpaChkbox = FindViewById<CheckBox>(Resource.Id.register_PdpaChkbox);
            TextView register_PdpaText = FindViewById<TextView>(Resource.Id.register_PdpaText);
            Button register_RegisterBtn = FindViewById<Button>(Resource.Id.register_RegisterBtn);

            RunOnUiThread(() =>
            {
                //  Set textfield text sizes to be same as button text sizes
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
                };

                //  Configure spinner adapter for gender dropdown
                register_GenderSpinner.Adapter = new SpinnerAdapter(this, genders);

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
                    pdpa.SetNeutralButton("Close", delegate
                    {
                        pdpa.Dispose();
                    });

                    var alert = pdpa.Show();
                    TextView alertMessage = (TextView)alert.Window.DecorView.FindViewById(Android.Resource.Id.Message);
                    alertMessage.SetTextSize(Android.Util.ComplexUnitType.Px, Convert.ToSingle(register_RegisterBtn.TextSize * 0.8));
                };
            });

            //  Intent to redirect to main menu page
            register_RegisterBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Main_Menu));
                StartActivity(intent);
            };

            //Implement CustomTheme ActionBar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Registration";

            //Set backarrow as Default
            ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Sign_In));
            StartActivity(intent);

            Toast.MakeText(this, "Sign In" + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

        //  Method to call DatePickerFragment
        private void SelectDate(EditText register_DOBField)
        {
            DatePickerFragment fragment = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                register_DOBField.Text = time.ToLongDateString();
            });

            fragment.Show(FragmentManager, TAG);
        }

        //  List of genders to populate spinner adapter
        private string[] genders =
        {
            "Gender",
            "Male",
            "Female"
        };
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