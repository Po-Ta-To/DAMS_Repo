using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Hospital_Details : AppCompatActivity
    {
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
            TextView hospDetails_ContactLabel = FindViewById<TextView>(Resource.Id.hospDetails_ContactLabel);
            TextView hospDetails_ContactText = FindViewById<TextView>(Resource.Id.hospDetails_ContactText);
            Button hospDetails_TreatmentsBtn = FindViewById<Button>(Resource.Id.hospDetails_TreatmentsBtn);
            Button hospDetails_RequestBtn = FindViewById<Button>(Resource.Id.hospDetails_RequestBtn);

            RunOnUiThread(() =>
            {
                //  Set label typeface to be same as button typeface
                hospDetails_HospitalText.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                hospDetails_AddressLabel.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                hospDetails_OpeningHoursLabel.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                hospDetails_ContactLabel.SetTypeface(hospDetails_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
            });

            //Implement CustomTheme ActionBar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.HCDetails_title);
            SetSupportActionBar(toolbar);

            //Set backarrow as Default
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
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
