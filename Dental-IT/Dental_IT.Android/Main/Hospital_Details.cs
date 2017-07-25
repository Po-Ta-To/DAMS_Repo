using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Hospital_Details : AppCompatActivity
    {
        private string hospitalName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to request appointment layout
            SetContentView(Resource.Layout.Hospital_Details);

            //  Create widgets
            TextView hospDetails_HospitalText = FindViewById<TextView>(Resource.Id.hospDetails_HospitalText);
            TextView hospDetails_AddressLabel = FindViewById<TextView>(Resource.Id.hospDetails_AddressLabel);
            TextView hospDetails_AddressText = FindViewById<TextView>(Resource.Id.hospDetails_AddressText);
            TextView hospDetails_OpeningHoursLabel = FindViewById<TextView>(Resource.Id.hospDetails_OpeningHoursLabel);
            TextView hospDetails_OpeningHoursText = FindViewById<TextView>(Resource.Id.hospDetails_OpeningHoursText);
            TextView hospDetails_TelephoneLabel = FindViewById<TextView>(Resource.Id.hospDetails_TelephoneLabel);
            TextView hospDetails_TelephoneText = FindViewById<TextView>(Resource.Id.hospDetails_TelephoneText);
            TextView hospDetails_EmailLabel = FindViewById<TextView>(Resource.Id.hospDetails_EmailLabel);
            TextView hospDetails_EmailText = FindViewById<TextView>(Resource.Id.hospDetails_EmailText);
            Button hospDetails_TreatmentsBtn = FindViewById<Button>(Resource.Id.hospDetails_TreatmentsBtn);
            Button hospDetails_RequestBtn = FindViewById<Button>(Resource.Id.hospDetails_RequestBtn);

            //  Check if redirected from search hospital or offered by
            Intent i = this.Intent;

            if (i.GetStringExtra("details_HospitalName") != null)
            {
                //  Receive data from search hospital
                hospitalName = i.GetStringExtra("details_HospitalName");
            }
            else if (i.GetStringExtra("offered_HospitalName") != null)
            {
                //  Receive data from offered by
                hospitalName = i.GetStringExtra("offered_HospitalName");
            }
            else
            {
                hospitalName = "Data not available";
            }

            RunOnUiThread(() =>
            {
                //  Set label typeface to be same as button typeface
                hospDetails_HospitalText.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                hospDetails_AddressLabel.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                hospDetails_OpeningHoursLabel.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                hospDetails_TelephoneLabel.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                hospDetails_EmailLabel.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);

                //  Set hospital name
                hospDetails_HospitalText.Text = hospitalName;

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.hospDetails_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });

            //  Intent to redirect to request page
            hospDetails_RequestBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Request_Appointment));
                intent.PutExtra("request_HospitalName", hospDetails_HospitalText.Text);
                StartActivity(intent);
            };
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Select_Hospital));
            StartActivity(intent);

            Toast.MakeText(this, "Select Hospital/Clinics",
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}
