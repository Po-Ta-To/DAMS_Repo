﻿using System;
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
    public class Appointment_Details : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to appointment details layout
            SetContentView(Resource.Layout.Appointment_Details);

            //  Create widgets
            TextView apptDetails_HospitalLabel = FindViewById<TextView>(Resource.Id.apptDetails_HospitalLabel);
            TextView apptDetails_HospitalText = FindViewById<TextView>(Resource.Id.apptDetails_HospitalText);
            TextView apptDetails_DateLabel = FindViewById<TextView>(Resource.Id.apptDetails_DateLabel);
            TextView apptDetails_DateText = FindViewById<TextView>(Resource.Id.apptDetails_DateText);
            TextView apptDetails_DentistLabel = FindViewById<TextView>(Resource.Id.apptDetails_DentistLabel);
            TextView apptDetails_DentistText = FindViewById<TextView>(Resource.Id.apptDetails_DentistText);
            TextView apptDetails_SessionLabel = FindViewById<TextView>(Resource.Id.apptDetails_SessionLabel);
            TextView apptDetails_SessionText = FindViewById<TextView>(Resource.Id.apptDetails_SessionText);
            TextView apptDetails_TreatmentLabel = FindViewById<TextView>(Resource.Id.apptDetails_TreatmentLabel);
            TextView apptDetails_TreatmentText = FindViewById<TextView>(Resource.Id.apptDetails_TreatmentText);
            TextView apptDetails_StatusLabel = FindViewById<TextView>(Resource.Id.apptDetails_StatusLabel);
            TextView apptDetails_StatusText = FindViewById<TextView>(Resource.Id.apptDetails_StatusText);
            Button apptDetails_UpdateBtn = FindViewById<Button>(Resource.Id.apptDetails_UpdateBtn);
            Button apptDetails_DeleteBtn = FindViewById<Button>(Resource.Id.apptDetails_DeleteBtn);

            RunOnUiThread(() =>
            {
                //  Set label typeface to be same as button typeface
                apptDetails_HospitalLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_DateLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_DentistLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_SessionLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_TreatmentLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_StatusLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
            });


            //Implement CustomTheme ActionBar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.apptdetails_title);
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
            Intent intent = new Intent(this, typeof(Main_Menu));
            StartActivity(intent);

            Toast.MakeText(this, "Main Menu",
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}