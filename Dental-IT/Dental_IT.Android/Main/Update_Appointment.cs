using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Views.InputMethods;
using Android.Preferences;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Dental_IT.Model;
using System.Collections.Generic;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Update_Appointment : AppCompatActivity
    {
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private List<Dentist> dentists = new List<Dentist>() { new Dentist() };
        private List<Session> sessions = new List<Session>() { new Session() };
        private int[] dentistIDArr;
        private int[] sessionIDArr;
        private int[] treatmentIDArr;
        private int userID;
        private string accessToken;

        private TextView update_HospitalLabel;
        private EditText update_HospitalField;
        private TextView update_DateLabel;
        private EditText update_DateField;
        private TextView update_DentistLabel;
        private Spinner update_DentistSpinner;
        private TextView update_SessionLabel;
        private Spinner update_SessionSpinner;
        private Button update_TreatmentsBtn;
        private TextView update_RemarksLabel;
        private EditText update_RemarksField;
        private Button update_SubmitBtn;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to update appointment layout
            SetContentView(Resource.Layout.Update_Appointment);

            //  Receive data from select_hospital
            string hospitalName = Intent.GetStringExtra("update_HospitalName") ?? "Data not available";

            //  Create widgets
            update_HospitalLabel = FindViewById<TextView>(Resource.Id.update_HospitalLabel);
            update_HospitalField = FindViewById<EditText>(Resource.Id.update_HospitalField);
            update_DateLabel = FindViewById<TextView>(Resource.Id.update_DateLabel);
            update_DateField = FindViewById<EditText>(Resource.Id.update_DateField);
            update_DentistLabel = FindViewById<TextView>(Resource.Id.update_DentistLabel);
            update_DentistSpinner = FindViewById<Spinner>(Resource.Id.update_DentistSpinner);
            update_SessionLabel = FindViewById<TextView>(Resource.Id.update_SessionLabel);
            update_SessionSpinner = FindViewById<Spinner>(Resource.Id.update_SessionSpinner);
            update_TreatmentsBtn = FindViewById<Button>(Resource.Id.update_TreatmentsBtn);
            update_RemarksLabel = FindViewById<TextView>(Resource.Id.update_RemarksLabel);
            update_RemarksField = FindViewById<EditText>(Resource.Id.update_RemarksField);
            update_SubmitBtn = FindViewById<Button>(Resource.Id.update_SubmitBtn);

            //  Shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            //  Get intents
            Intent i = Intent;

            //  If redirected from hospital details page (New update)
            if (i.GetStringExtra("update_Appointment") != null)
            {
                //  Remove old shared preferences
                RemoveFromPreferences(prefs, editor);

                //  Receive hospital name data from intent
                hosp = JsonConvert.DeserializeObject<Hospital>(i.GetStringExtra("newRequest_Hospital"));
            }
            else
            {
                //  Receive data from shared preferences
                hosp = JsonConvert.DeserializeObject<Hospital>(prefs.GetString("hospital", "null"));
                request_DateField.Text = prefs.GetString("date", GetString(Resource.String.select_date));
            }

            RunOnUiThread(() =>
            {
                //  Set textfield text sizes to be same as textview text sizes
                update_HospitalField.SetTextSize(Android.Util.ComplexUnitType.Px, update_HospitalLabel.TextSize);
                update_DateField.SetTextSize(Android.Util.ComplexUnitType.Px, update_HospitalLabel.TextSize);
                update_RemarksField.SetTextSize(Android.Util.ComplexUnitType.Px, update_HospitalLabel.TextSize);

                //  Set label typeface to be same as button typeface
                update_HospitalLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_DateLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_DentistLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_SessionLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_RemarksLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);

                //  Set hospital name
                update_HospitalField.Text = hospitalName;

                //  Configure spinner adapter for dentist and session dropdowns
                //update_DentistSpinner.Adapter = new SpinnerAdapter(this, dentists, false);
                //update_SessionSpinner.Adapter = new SpinnerAdapter(this, sessions, false);

                //  Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.update_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });

            //  Intent to redirect to calendar page
            update_DateField.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Calendar_Select));
                StartActivity(intent);
            };

            //  Handle select treatments button
            update_TreatmentsBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Select_Treatment));
                StartActivity(intent);
            };

            //  Handle update button
            update_SubmitBtn.Click += delegate
            {
                //  Close keyboard
                InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                //  Retrieve access token
                if (prefs.Contains("token"))
                {
                    accessToken = prefs.GetString("token", "");
                }

                // GET the ApptI


                Intent intent = new Intent(this, typeof(My_Appointments));
                StartActivity(intent);
            };
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Appointment_Details));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }

        //  List of dentists to populate spinner adapter
        private string[] dentists =
        {
            "Select dentist",
            "Dentist A",
            "Dentist B",
            "Dentist C",
            "Dentist D",
            "Dentist E",
            "Dentist F",
            "Dentist G",
            "Dentist H",
            "Dentist I",
            "Dentist J",
            "Dentist K",
            "Dentist L"
        };

        //  List of sessions to populate spinner adapter
        private string[] sessions =
        {
            "Select session",
            "Session 1",
            "Session 2",
            "Session 3",
            "Session 4"
        };
    }
}